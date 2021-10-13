import React, { createContext, useContext, useReducer } from "react";
export const StateContext = createContext();

export const StateProvider = ({ reducer, initialState, children }) => (
  <StateContext.Provider value = {useReducer(reducer, initialState)}>
    {children}
  </StateContext.Provider>
);

// We create this f, With 'const useStateValue' we have acces to all var globals on my context
export const useStateValue = () => useContext(StateContext);
