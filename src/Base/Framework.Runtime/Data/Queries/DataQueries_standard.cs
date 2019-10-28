namespace BindOpen.Framework.Runtime.Data.Queries
{

    /// <summary>
    /// This class contains data queries for the 'Standard' extension.
    /// </summary>
    public static partial class DataQueries_standard
    {

        // ------------------------------------------
        // MESSAGES
        // ------------------------------------------

        #region Messages


        //// ****************** MESSAGES *****************************

        ///// <summary>
        ///// Builds the following query: Insert a new internal message.
        ///// </summary>
        ///// <param name="aBasicMessageFromId">Indicates the ID of the sender of the simple message to insert is in html.</param>
        ///// <param name="aBasicMessageFromName">Indicates the name of the sender of the simple message to insert is in html.</param>
        ///// <param name="aBasicMessageSubjectLabel">Indicates the subject label of the simple message to insert is in html.</param>
        ///// <param name="aBasicMessageBodyLabel">Indicates the body label of the simple message to insert is in html.</param>
        ///// <param name="aBasicMessageIsBodyHtml">Indicates whether the body of the simple message to insert is in html.</param>
        ///// <param name="simpleMessageAttachedFiles">Attached files of the simple message to insert.</param>
        ///// <param name="aBasicMessageDetail">Detail of the simple message to insert.</param>
        ///// <returns>The built query.</returns>
        //public static BasicDbDataQuery GetDbDataQuery_InsertInternalMessage(
        //    String aBasicMessageFromId,
        //    String aBasicMessageFromName,
        //    String aBasicMessageSubjectLabel,
        //    String aBasicMessageBodyLabel,
        //    Boolean aBasicMessageIsBodyHtml,
        //    List<string> simpleMessageAttachedFiles,
        //    DataElementSet aBasicMessageDetail)
        //{
        //    BasicDbDataQuery aDbQuery = new BasicDbDataQuery()
        //    {
        //        Name = "GetDbDataQuery_InsertInternalMessage",
        //        DataModule = "ENT_CENTRAL_DB",
        //        DataTable = "INTERNALMESSAGE",
        //        Type = DbDataQuery.DbDataQueryType.Insert,
        //        Fields =
        //        {                    
        //            new DbField()
        //            {
        //                Name="CREATIONDATE",
        //                Value=new DataExpression("=$SQLGETCURRENTDATE()")
        //            },
        //            new DbField()
        //            {
        //                Name="MODIFICATIONDATE",
        //                Value=new DataExpression("=$SQLGETCURRENTDATE()")
        //            },
        //            new DbField()
        //            {
        //                Name="FROMPLATFORMUSER_ID",
        //                ValueType= DataValueType.Number,
        //                Value=new DataExpression(aBasicMessageFromId)
        //            },
        //            new DbField()
        //            {
        //                Name="FROMPLATFORMUSER_CONTACTNAME",
        //                ValueType= DataValueType.Text,
        //                Value=new DataExpression(aBasicMessageFromName)
        //            },
        //            new DbField()
        //            {
        //                Name="SUBJECT",
        //                ValueType= DataValueType.Text,
        //                Value=new DataExpression(aBasicMessageSubjectLabel)
        //            },
        //            new DbField()
        //            {
        //                Name="BODY",
        //                ValueType= DataValueType.Text,
        //                Value=new DataExpression(aBasicMessageBodyLabel)
        //            },
        //            new DbField()
        //            {
        //                Name="ISBODYHTML",
        //                ValueType= DataValueType.Boolean,
        //                Value=new DataExpression(aBasicMessageIsBodyHtml.ToString())
        //            },
        //            new DbField()
        //            {
        //                Name="ATTACHMENTFILEPATHS",
        //                ValueType= DataValueType.Text,
        //                Value=new DataExpression(string.Join(",",simpleMessageAttachedFiles))
        //            }
        //        }
        //    };
        //    if (aBasicMessageDetail!=null)
        //    {
        //        if (aBasicMessageDetail.HasElementWithName("KIND"))
        //            aDbQuery.Fields.Add(
        //                new DbField()
        //                {
        //                    Name = "KIND",
        //                    ValueType = DataValueType.Text,
        //                    Value = new DataExpression(aBasicMessageDetail.GetItem("KIND").GetValue(DataExpressionKind.Script, null, null).ToString())
        //                });
        //        if (aBasicMessageDetail.HasElementWithName("OBJECT_ID"))
        //            aDbQuery.Fields.Add(
        //                new DbField()
        //                {
        //                    Name = "OBJECT_ID",
        //                    ValueType = DataValueType.Text,
        //                    Value = new DataExpression(aBasicMessageDetail.GetItem("OBJECT_ID").GetValue(DataExpressionKind.Script, null, null).ToString())
        //                });
        //        if (aBasicMessageDetail.HasElementWithName("PARENT_ID"))
        //            aDbQuery.Fields.Add(
        //                new DbField()
        //                {
        //                    Name = "PARENT_ID",
        //                    ValueType = DataValueType.Text,
        //                    Value = new DataExpression(aBasicMessageDetail.GetItem("PARENT_ID").GetValue(DataExpressionKind.Script, null, null).ToString())
        //                });
        //    }

        //    return aDbQuery;
        //}

        ///// <summary>
        ///// Builds the following query: Insert a new internal message user relationship.
        ///// </summary>
        ///// <param name="aInternalMessageId">ID of the internal message.</param>
        ///// <param name="currentUserContactUserName">Destionation name of the internal message.</param>
        ///// <param name="currentUserContactUserId">The ID of the current user contact to link to the message.</param>
        ///// <returns>The built query.</returns>
        //public static BasicDbDataQuery GetDbDataQuery_InsertInternalMessageUserRelationship(
        //    long aInternalMessageId,
        //    String currentUserContactUserName,
        //    long currentUserContactUserId)
        //{
        //    BasicDbDataQuery aDbQuery = new BasicDbDataQuery()
        //    {
        //        Name = "GetDbDataQuery_InsertInternalMessageUserRelationship",
        //        DataModule = "ENT_CENTRAL_DB",
        //        DataTable = "INTERNALMESSAGE_USER_ASSIGNEDTO",
        //        Type = DbDataQuery.DbDataQueryType.Insert,
        //        Fields =
        //        {
        //            new DbField()
        //            {
        //                Name="INTERNALMESSAGE_ID",
        //                ValueType= DataValueType.Number,
        //                Value=new DataExpression(aInternalMessageId.ToString())
        //            },
        //            new DbField()
        //            {
        //                Name="PLATFORMUSER_ID",
        //                ValueType= DataValueType.Number,
        //                Value=new DataExpression(currentUserContactUserId.ToString())
        //            },
        //            new DbField()
        //            {
        //                Name="PLATFORMUSERWORKGROUP_ID",
        //                Value=new DataExpression("=$SQLNULL()")
        //            },
        //            new DbField()
        //            {
        //                Name="TONAME",
        //                ValueType= DataValueType.Text,
        //                Value=new DataExpression(currentUserContactUserName)
        //            },
        //            new DbField()
        //            {
        //                Name="ISREAD",
        //                ValueType= DataValueType.Boolean,
        //                Value=new DataExpression("FALSE")
        //            }
        //        }
        //    };
        //    return aDbQuery;
        //}

        ///// <summary>
        ///// Builds the following query: Insert a new message sending request.
        ///// </summary>
        ///// <param name="logFilePath">The path of the log file.</param>
        ///// <param name="aMaxTryNumber">The number maximum of tries for sending messages.</param>
        ///// <param name="status">The status of the message sending request.</param>
        ///// <returns>The built query.</returns>
        //public static BasicDbDataQuery GetDbDataQuery_InsertMessageSendingRequest(
        //    String logFilePath,
        //    int aMaxTryNumber,
        //    String status)
        //{
        //    BasicDbDataQuery aDbQuery = new BasicDbDataQuery()
        //    {
        //        Name = "GetDbDataQuery_InsertMessageSendingRequest",
        //        DataModule = "PTF_STANDARD_DB",
        //        DataTable = "MESSAGESESENDINGREQUEST",
        //        Type = DbDataQuery.DbDataQueryType.Insert,
        //        Fields =
        //        {
        //            new DbField()
        //            {
        //                Name="CREATIONDATE",
        //                Value=new DataExpression("=$SQLGETCURRENTDATE()")
        //            },
        //            new DbField()
        //            {
        //                Name="MODIFICATIONDATE",
        //                Value=new DataExpression("=$SQLGETCURRENTDATE()")
        //            },
        //            new DbField()
        //            {
        //                Name="LOGFILEPATH",
        //                ValueType= DataValueType.Text,
        //                Value=new DataExpression(logFilePath)
        //            },
        //            new DbField()
        //            {
        //                Name="REMAININGTRYNUMBER",
        //                ValueType= DataValueType.Number,
        //                Value=new DataExpression(aMaxTryNumber.ToString())
        //            },
        //            new DbField()
        //            {
        //                Name="STATUS",
        //                ValueType= DataValueType.Text,
        //                Value=new DataExpression(status.ToUpper())
        //            }
        //        }
        //    };
        //    return aDbQuery;
        //}

        #endregion

    }
}