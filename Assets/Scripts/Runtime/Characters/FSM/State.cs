namespace Runtime.Characters.FSM
{
    /// <summary>
    /// The template of FSM pattern
    /// </summary>
    /// <typeparam name="T">The entity that use the fsm pattern</typeparam>
    public interface IState<T>
    {
        public void Enter(T entity);

        public void Update(T entity);

        public void Exit(T entity);
    }
}