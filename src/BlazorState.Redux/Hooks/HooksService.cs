﻿using System;
using System.Collections.Generic;
using BlazorState.Redux.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorState.Redux.Hooks
{
    // TODO: add component to have hooks enabled
    public class HooksService : IHookService
    {
        private Dictionary<ComponentBase, ComponentState> _stateStore = new Dictionary<ComponentBase, ComponentState>();

        public (T, Action<T>) UseState<T>(T initialState, ComponentBase component)
        {
            if (!_stateStore.ContainsKey(component))
            {
                _stateStore.Add(component, new ComponentState());
            }

            var componentState = _stateStore[component];
            componentState.Add(initialState);
            var currentIdxClouser = componentState.CurrentIndex;
            return (componentState.GetNext<T>(), s => SetState(s, componentState, currentIdxClouser));
        }

        public void ComponentRendered(ComponentBase component)
        {
            if (_stateStore.ContainsKey(component))
            {
                var componentState = _stateStore[component];
                componentState.Finilize();
            }
        }

        public void ComponentDisposed(ComponentBase component)
        {
            if (_stateStore.ContainsKey(component))
            {
                _stateStore.Remove(component);
            }
        }

        private void SetState<T>(T newState, ComponentState componentState, int idx)
        {
            componentState.SetAtIndex(newState, idx);
        }
    }
}
