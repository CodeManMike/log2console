﻿using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
#if NET_FRAMEWORK
using System.Messaging;
#endif

using Log2Console.Log;


namespace Log2Console.Receiver
{
#if NET_FRAMEWORK
    [Serializable]
    [DisplayName("Windows Message Queue (MSMQ)")]
    public class MsmqReceiver : BaseReceiver
    {
        [NonSerialized]
        private MessageQueue _queue;

        [NonSerialized]
        private Timer _queueCreationCheckTimer;

        [NonSerialized] private const int QueueCheckTimerDelayAndInterval = 5000;


        private string _queueName = @".\private$\log";

        [Category("Configuration")]
        [DisplayName("Queue Name")]
        [DefaultValue(@".\private$\log")]
        [Description(@"Name of the queue to create.  I.e. .\private$\log-test")]
        public string QueueName
        {
            get { return _queueName; }
            set { _queueName = value; }
        }

        [Category("Configuration")]
        [DisplayName("Create Queue")]
        [Description("Determines how to handle queue creation.  If true and the queue does not exist it will be created.  If false and the queue does not exist the receiver will wait for the queue to be created.")]
        public bool Create { get; set; }

        [Category("Configuration")]
        public bool Transactional { get; set; }


        private bool _bulkProcessBackedUpMessages = true;

        [Category("Behavior")]
        [DefaultValue(true)]
        [DisplayName("Bulk Process Backed Up Messages")]
        [Description("If true multiple messages in the queue are processed as one update to the log viewer.  This improves the performance of the viewer")]
        public bool BulkProcessBackedUpMessages
        {
            get { return _bulkProcessBackedUpMessages;}
            set { _bulkProcessBackedUpMessages = value; }
        }


        [Browsable(false)]
        public override string SampleClientConfig
        {
            get
            {
                return
                    "Configuration for NLog:" + Environment.NewLine +
                    "<target name=\"queue\" type=\"MSMQ\" layout=\"${log4jxmlevent}\" " + Environment.NewLine +
                    "\tqueue = \".\\private$\\nlog\" recoverable= \"true\"" + Environment.NewLine +
                    "\tlabel = \"${logger}\" />" + Environment.NewLine +
                    Environment.NewLine + Environment.NewLine +
                    "Configuration for log4net:" + Environment.NewLine +
                    Environment.NewLine +
                    "NOTE:  log4net (1.2.10) does not include an MSMQ appender.  The following configuration is based on the MSMQ Appender in '.\\examples\\net\\1.0\\Appenders\\SampleAppendersApp\\cs\\src' that is included in the log4net download."+
                    Environment.NewLine + Environment.NewLine +
                    "<appender name=\"MsmqAppender\" type=\"SampleAppendersApp.Appender.MsmqAppender, SampleAppendersApp\">" +
                    Environment.NewLine +
                    "\t<queueName value=\".\\Private$\\log-test\" />" + Environment.NewLine +
                    "\t<labelLayout value=\"LOG [%level] %date\" />" + Environment.NewLine +
                    "\t<layout type=\"log4net.Layout.XmlLayoutSchemaLog4j\" />" + Environment.NewLine +
                    "</appender>";
            }
        }


        /// <summary>
        ///
        /// </summary>
        public override void Initialize()
        {
            if (!MessageQueue.Exists(QueueName))
            {
                if (Create)
                {
                    MessageQueue.Create(QueueName, Transactional);
                }
                else
                {
                    /*
                     * Start the queue check timer.  Should the time be configurable?
                     */
                    _queueCreationCheckTimer = new Timer(QueueCreationCheckTimerFunction, this,
                                                         QueueCheckTimerDelayAndInterval,
                                                         QueueCheckTimerDelayAndInterval);
                    return;
                }
            }

            Start();
        }



        /// <summary>
        ///
        /// </summary>
        private void Start()
        {

            _queue = new MessageQueue(QueueName);

            _queue.ReceiveCompleted += delegate(Object source, ReceiveCompletedEventArgs asyncResult)
            {
                try
                {
                    // End the asynchronous receive operation.
                    Message m = ((MessageQueue)source).EndReceive(asyncResult.AsyncResult);

                    if (Notifiable != null)
                    {
                        string loggingEvent = Encoding.ASCII.GetString(((MemoryStream)m.BodyStream).ToArray());
                        LogMessage logMsg = ReceiverUtils.ParseLog4JXmlLogEvent(loggingEvent, "MSMQLogger");
                        logMsg.LoggerName = string.Format("{0}_{1}", QueueName.TrimStart('.'), logMsg.LoggerName);
                        logMsg.RootLoggerName = QueueName;
                        Notifiable.Notify(logMsg);
                    }


                    if (BulkProcessBackedUpMessages)
                    {
                        Message[] all = ((MessageQueue) source).GetAllMessages();
                        if (all.Length > 0)
                        {
                            int numberofmessages = all.Length > 1000 ? 1000 : all.Length;

                            LogMessage[] logs = new LogMessage[numberofmessages];

                            for (int i = 0; i < numberofmessages; i++)
                            {
                                Message thisone = ((MessageQueue) source).Receive();

                                string loggingEvent =
                                    Encoding.ASCII.GetString(((MemoryStream) thisone.BodyStream).ToArray());
                                LogMessage logMsg = ReceiverUtils.ParseLog4JXmlLogEvent(loggingEvent, "MSMQLogger");
                                logMsg.LoggerName = string.Format("{0}_{1}", QueueName.TrimStart('.'), logMsg.LoggerName);
                                logs[i] = logMsg;
                            }

                            Notifiable.Notify(logs);
                        }
                    }

                    ((MessageQueue)source).BeginReceive();
                }
                catch (MessageQueueException)
                {
                    // Handle sources of MessageQueueException.
                }
            };

            _queue.BeginReceive();
        }




        /// <summary>
        ///
        /// </summary>
        public override void Terminate()
        {
            /*
             * Are we going to have any issues if we are processing a receive complete or will
             * MSMQ protect us?
             */
            if (_queue != null)
            {
                _queue.Close();
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        private static void QueueCreationCheckTimerFunction(object state)
        {
            //TODOCJH:  If this timer gets called then we did not finish the job before the maximum allowable time.
            //_logger.Fatal("JobMaxExecutionTimerFunction");

            MsmqReceiver rcv = state as MsmqReceiver;
            if ((rcv != null) && MessageQueue.Exists(rcv.QueueName))
            {
                rcv._queueCreationCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
                rcv._queueCreationCheckTimer.Dispose();
                rcv.Start();
            }
        }
    }
#else
    // Dummy implementation for .NET 8+ since System.Messaging is not available
    [Serializable]
    [DisplayName("Windows Message Queue (MSMQ) - Not Available in .NET 8+")]
    public class MsmqReceiver : BaseReceiver
    {
        [Category("Configuration")]
        [DisplayName("Queue Name")]
        [DefaultValue(@".\private$\log")]
        public string QueueName { get; set; } = @".\private$\log";

        [Category("Configuration")]
        [DisplayName("Create Queue")]
        public bool Create { get; set; }

        [Category("Configuration")]
        public bool Transactional { get; set; }

        [Category("Behavior")]
        [DefaultValue(true)]
        [DisplayName("Bulk Process Backed Up Messages")]
        public bool BulkProcessBackedUpMessages { get; set; } = true;

        [Browsable(false)]
        public override string SampleClientConfig => "MSMQ functionality is not available in .NET 8+";

        public override void Initialize()
        {
            // System.Messaging is not available in .NET 8+
            if (Notifiable != null)
            {
                LogMessage logMsg = new LogMessage()
                {
                    LoggerName = "MsmqReceiver",
                    Message = "MSMQ functionality is not available in .NET 8+. Please use alternative receivers.",
                    Level = LogLevels.Instance[(int)LogLevel.Error]
                };
                Notifiable.Notify(logMsg);
            }
        }

        public override void Terminate() { }
    }
#endif
}
