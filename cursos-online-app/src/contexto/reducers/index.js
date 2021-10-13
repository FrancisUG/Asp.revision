//we import the reducers we have created
import sesionUsuarioReducer from "./sesionUsuarioReducer";
import openSnackbarReducer from "./openSnackbarReducer";

//We create a f allow them to be unified, Indicamos q pasaran 3 param 'sesionUsuario'-- 'openSnackbar'
export const mainReducer = ({ sesionUsuario, openSnackbar }, action) => {
   
  return {
    sesionUsuario: sesionUsuarioReducer(sesionUsuario, action),
    openSnackbar: openSnackbarReducer(openSnackbar, action)
  }
}
