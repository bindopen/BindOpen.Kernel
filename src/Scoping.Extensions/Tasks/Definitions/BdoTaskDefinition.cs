using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Scoping.Tasks
{
    /// <summary>
    /// This class represents a task definition.
    /// </summary>
    /// <seealso cref="BdoConfiguration"/>
    public class BdoTaskDefinition : BdoExtensionDefinition, IBdoTaskDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// Indicates whether this instance is executable.
        /// </summary>
        public bool IsExecutable { get; set; } = true;

        /// <summary>
        /// Maximum index of this instance.
        /// </summary>
        public float MaximumIndex { get; set; } = 100;

        /// <summary>
        /// Output specification of this instance.
        /// </summary>
        public ITBdoSet<IBdoSpec> OutputSpecs { get; set; }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskDefinition class. 
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoTaskDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition) : base(name, "task_", extensionDefinition)
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return UniqueName;
        }

        #endregion

        // --------------------------------------------------
        // CHECK
        // --------------------------------------------------

        #region Check

        /// <summary>
        /// Repairs this instance basing on the specified definition task.
        /// </summary>
        /// <param key="taskDefinition">The definition task to consider.</param>
        public void Repair(IBdoTaskDefinition taskDefinition)
        {
            if (taskDefinition != null)
            {
                if (taskDefinition.Title != null)
                    Title = taskDefinition.Title.Clone() as TBdoDictionary<string>;
                if (taskDefinition.Description != null)
                    Description = taskDefinition.Description.Clone() as TBdoDictionary<string>;

                //InputSpecification.Repair(
                //    taskDefinition.InputSpecification,
                //    BdoElementSpec.__Arenames.ToList().Excluding(new[] { nameof(DataAreaKind.Items) }).ToArray());
                //OutputSpecification.Repair(
                //    taskDefinition.OutputSpecification,
                //    BdoElementSpec.__Arenames.ToList().Excluding(new[] { nameof(DataAreaKind.Items) }).ToArray());
            }
        }
        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            OutputSpecs?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}