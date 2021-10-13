//Imprtamos el arch d HttpCliente
import HttpCliente from "../servicios/HttpCliente";
import axios from 'axios';


export const registrarUsuario = (usuario) => {
  return new Promise((resolve, eject) => {
  HttpCliente.post("/usuario/registrar", usuario).then((response) => {
      resolve(response);
      //Se confirma q el proceso con el servidor ha concluido
    });
  });
};


export const obtenerUsuarioActual = (dispatch) => {
  //esta f no recibe ningun param, SOLO el request, el mismo action le pasa el token con las configuracns q hicimos
  return new Promise((resolve, eject) => {
   
    HttpCliente.get("/usuario").then((response) => {
      console.log('response ', response);
      if(response.data && response.data.imagenPerfil){
          let fotoPerfil = response.data.imagenPerfil;
          const nuevoFile = 'data:image/' + fotoPerfil.extension + ';base64,' + fotoPerfil.data;
          response.data.imagenPerfil = nuevoFile;
      }
      
      dispatch({
        type: "INICIAR_SESION",
        sesion: response.data,
        autenticado: true,
      });

      resolve(response);
    }).catch(error => {
      console.log('error actualizar', error);
      resolve(error);
  });
});
 
}

export const actualizarUsuario = (usuario, dispatch) => {
  return new Promise((resolve, eject) => {
    HttpCliente.put("/usuario", usuario).then((response) => {
      resolve(response);
    })
    .catch(error => {
      resolve(error.response);
    })
  });
};


//Le pasamos un obj d tipo usuario, retornara una promesa, invok a HttpCliente, invok a 1 meth q permite login 'post'
export const loginUsuario = (usuario, dispatch) => {
  return new Promise((resolve, eject) => {
    HttpCliente.post("/usuario/login", usuario).then((response) => {
      resolve(response);
    });
  });
};
