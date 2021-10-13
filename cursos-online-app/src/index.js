import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";

//Importamos estos 3 objs, al llamar a 'mainReducer' x defecto se sabe q es al index  de "reducers"
//Los reucers tienen acceso a los valores globales
import { initialState } from "./contexto/initialState";
import { StateProvider } from "./contexto/store";
import { mainReducer } from "./contexto/reducers";

ReactDOM.render(
  <React.StrictMode>
    <StateProvider initialState = { initialState } reducer = {mainReducer}>
      <App />
    </StateProvider>
  </React.StrictMode>,
  document.getElementById("root")
);

//1ro invocamos al StateProvider, 2do el 'reducer' esta representado por "mainReducer"

reportWebVitals();

