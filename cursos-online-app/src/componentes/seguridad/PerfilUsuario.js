import {
  Button,
  Container,
  Grid,
  TextField,
  Typography,
} from "@material-ui/core";
import React, { useState, useEffect } from "react";
import {
  obtenerUsuarioActual,
  actualizarUsuario
  
} from "../../actions/UsuarioAction";
//import ImageUploader from "react-images-upload";
//import reactFoto from "../../logo.svg";
//import { v4 as uuidv4 } from "uuid";
//import { obtenerDataImagen } from "../../actions/ImagenAction";
import style from "../Tool/Style";
import { useStateValue } from "../../contexto/store";

const PerfilUsuario = () => {
  const [{ sesionUsuario }, dispatch] = useStateValue();
  const [usuario, setUsuario] = useState({
    
    //Le indicamos q estos son los valores q quiero mapear
    nombreCompleto: "",
    email: "",
    password: "",
    confirmarPassword: "",
    username: "",
  });

  const ingresarValoresMemoria = (e) => {
    const { name, value } = e.target;

    setUsuario((anterior) => ({
      ...anterior,
      [name]: value,
    }));

    //Cuando esto se ejecute mantendra todos los datos anteriores dl form y solo cambiara aquella c. d texto q el usuario ingresa data
  };


  //Vamos hace q se pinte la data cuando la accion se haya realizado
  useEffect(() => {
    setUsuario(sesionUsuario.usuario);
    
   
    obtenerUsuarioActual(dispatch).then((response) => {
      
      console.log(
        'Esta es la data del objeto response del usuario actual', response);
      setUsuario(response.data);
      
    });
  }, []);
  //-->>>>

  const guardarUsuario = (e) => {
    e.preventDefault();
    console.log('usuario beofre send', usuario);
    actualizarUsuario(usuario, dispatch).then((response) => {
      //console.log('se actualizo el usuario', response);

      if (response.status === 200) {
        dispatch({
          type: "OPEN_SNACKBAR",
          openMensaje: {
            open: true,
            mensaje: "Se guardaron exitosamente los cambios en Perfil Usuario",
          }
        })
        window.localStorage.setItem("token_seguridad", response.data.token);
      }else {
        dispatch({
          type: "OPEN_SNACKBAR",
          openMensaje: {
            open: true,
            mensaje:
              "Errores al intentar guardar en : " +
              Object.keys(response.data.errors),
          },
        });
      }
      
    })
  }

  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Perfil de usuario
        </Typography>
      </div>
      <form style={style.form}>
        <Grid container spacing={2}>
          <Grid item xs={12} md={12}>
            <TextField
              name="nombreCompleto"
              value={usuario.nombreCompleto || ""}
              variant="outlined"
              onChange={ingresarValoresMemoria}
              fullWidth
              label="Ingrese nombre y apellidos"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField
              name="username"
              value={usuario.username || ""}
              variant="outlined"
              onChange={ingresarValoresMemoria}
              fullWidth
              label="Ingrese username"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField
              name="email"
              value={usuario.email || ""}
              variant="outlined"
              onChange={ingresarValoresMemoria}
              fullWidth
              label="Ingrese email"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField
              name="password"
              value={usuario.password || ""}
              type="password"
              onChange={ingresarValoresMemoria}
              variant="outlined"
              fullWidth
              label="Ingrese password"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField
              name="confirmarPassword"
              value={usuario.confirmarPassword || ""}
              type="password"
              onChange={ingresarValoresMemoria}
              variant="outlined"
              fullWidth
              label="Confirme password"
            />
          </Grid>
        </Grid>

        <Grid container justifyContent="center">
          <Grid item xs={12} md={6}>
            <Button
              type="submit"
              //Le indicamos q invoke al metodo 'guardarUsuario' para implementarlo en la prt superior
              onClick={guardarUsuario}
              fullWidth
              variant="contained"
              size="large"
              color="primary"
              style={style.submit}
            >
              Guardar Datos
            </Button>
          </Grid>
        </Grid>
      </form>
    </Container>
  )
};

export default PerfilUsuario;
