
const initialState = {
  open: false,
  mensaje: ""
};

//Creamos una f q maneje esa data
const openSnackbarReducer = (state = initialState, action) => {
  console.log("action", action);
  switch (action.type) {
   
    case "OPEN_SNACKBAR" :
      return {
        ...state,
        open: action.openMensaje.open,
        mensaje: action.openMensaje.mensaje
      };
    default :
      return state;
    //else retorna el state actual
  }
}

export default openSnackbarReducer;
