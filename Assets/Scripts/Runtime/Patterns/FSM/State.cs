namespace Runtime.Patterns.FSM
{
    /// <summary>
    /// The template of FSM pattern
    /// </summary>
    /// <typeparam name="T">The entity that use the fsm pattern</typeparam>
    public interface IState<T>
    {
        /// <summary>
        /// Enter the state
        /// </summary>
        /// <param name="entity">The entity that use the fsm pattern</param>
        public void Enter(T entity);
        /// <summary>
        /// Update the state
        /// </summary>
        /// <param name="entity">The entity that use the fsm pattern</param>
        public void Update(T entity);
        /// <summary>
        /// FixedUpdate the state
        /// </summary>
        /// <param name="entity">The entity that use the fsm pattern</param>
        /// <remarks>Use this method for physics</remarks>
        /// <example>For example:
        /// <code>
        /// public void FixedUpdate(T entity) {
        ///     entity.Rigidbody.AddForce(Vector3.up * 10f);
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="UnityEngine.MonoBehaviour.FixedUpdate"/>
        public void FixedUpdate(T entity);
        /// <summary>
        /// Exit the state
        /// </summary>
        /// <param name="entity">The entity that use the fsm pattern</param>
        public void Exit(T entity);
    }
}