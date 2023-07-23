namespace Runtime.Characters.FSM
{
    /// <summary>
    /// The template of FSM pattern
    /// </summary>
    /// <typeparam name="T">The entity that use the fsm pattern</typeparam>
    public class State<T>
    {
        public virtual void Enter(T entity)
        {
            
        }

        public virtual void Execute(T entity)
        {
            
        }

        public virtual void Exit(T entity)
        {
            
        }
    }
}