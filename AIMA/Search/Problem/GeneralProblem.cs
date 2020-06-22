using System;
using System.Collections.Generic;

namespace AIMA.Search
{
    public delegate double StepCostFunction<in TState, in TAction>(TState state, TAction action, TState sDelta);
    
    public class GeneralProblem<TState, TAction> : IProblem<TState, TAction>
    {
        public static readonly StepCostFunction<TState, TAction> One = (state, action, delta) => 1.0;
        
        private readonly StepCostFunction<TState, TAction> stepCostFunction;
        private readonly Func<TState, IEnumerable<TAction>> actionsFunction;
        private readonly Func<TState, TAction, TState> resultFunction;
        private readonly Predicate<TState> goalFunction;
        
        public TState InitialState { get; private set; }

        public GeneralProblem(TState initialState, Func<TState, IEnumerable<TAction>> actionsFn, 
            Func<TState, TAction, TState> resultFn,
            Predicate<TState> goalTest) : this(initialState, actionsFn, resultFn, goalTest, One)
        {
            
        }
        
        public GeneralProblem(TState initialState, 
            Func<TState, IEnumerable<TAction>> actionsFn, 
            Func<TState, TAction, TState> resultFn,
            Predicate<TState> goalTest,
            StepCostFunction<TState, TAction> stepCostFunction)
        {
            InitialState = initialState;
            this.actionsFunction = actionsFn;
            this.resultFunction = resultFn;
            this.goalFunction = goalTest;
            this.stepCostFunction = stepCostFunction;
        }
        
        public IEnumerable<TAction> GetActions(TState state)
        {
            return actionsFunction.Invoke(state);
        }

        public TState GetResult(TState state, TAction action)
        {
            return resultFunction.Invoke(state, action);
        }

        public double GetStepCosts(TState state, TAction action, TState stateDelta)
        {
            return stepCostFunction.Invoke(state, action, stateDelta);
        }

        public bool GoalTest(TState state)
        {
            return goalFunction.Invoke(state);
        }
    }
}