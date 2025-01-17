﻿using BindOpen.Data;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoLog :
        IIdentified, INamed,
        ITitled, IDescribed,
        ITTreeNode<IBdoLog>,
        IBdoDetailed, IBdoObject
    {
        /// <summary>
        /// 
        /// </summary>
        string ResultCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration TaskConfig { get; set; }

        /// <summary>
        /// Creates a new instance of IBdoLog.
        /// </summary>
        /// <returns></returns>
        IBdoLog NewLog();

        IBdoLog InsertChild(
            BdoEventLevels kind,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <param key="eventLevel"></param>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <returns></returns>
        IBdoLog AddChild(
            IBdoLog childLog = null,
            BdoEventLevels kind = BdoEventLevels.Any,
            string title = null,
            string description = null,
            DateTime? date = null,
            string resultCode = null);

        // Events

        /// <summary>
        /// 
        /// </summary>
        /// <param key="kind"></param>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
        IBdoLog AddEvent(
            BdoEventLevels kind,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        bool HasEvent(bool isRecursive = true, params BdoEventLevels[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        int RemoveEvents(bool isRecursive = true, params BdoEventLevels[] kinds);

        // Logs

        /// <summary>
        /// Creates a new instance of IBdoLog.
        /// </summary>
        /// <returns></returns>
        void Sanitize();

        // Execution

        /// <summary>
        /// 
        /// </summary>
        IBdoProcessExecution Execution { get; set; }
    }
}