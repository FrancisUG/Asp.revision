import React, { useState } from "react";
import {
  Button,
  Container,
  Grid,
  TextField,
  Typography,
} from "@material-ui/core";
import style from "../Tool/Style";
import { registrarUsuario } from "../../actions/UsuarioAction";

//F q representa nuestro 1er comp REACT
//declaramos la f como const
const RegistrarUsuario = () => {
 
  const [usuario, setUsuario] = useState({
    NombreCompleto: '',
    Email: '',
    Password: '',
    ConfirmarPassword: '',
    Username: '',
  });

  //Creamos la funcion q va disparar los valores al virtual DOM de REACT-- "e" = al contenido d la caja d texto
  const ingresarValoresMemoria = (e) => {
    const { name, value } = e.target;

    
    setUsuario((anterior) => ({
      ...anterior,
      [name]: value
    }))

  
  };

  //Metodo para el evento Button, se le pasa 'e' q represent el obj boton
  const registrarUsuarioBoton = (e) => {
 
    e.preventDefault();


   
    registrarUsuario(usuario).then((response) => {
      console.log("Se registro exitosamente el usuario", response);
      

      window.localStorage.setItem("token_seguridad", response.data.token);
    });
  };

  //Creamos la interfaz grafica d este comp
  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Registro de Usuario
        </Typography>

        {/*item 'xs'= como max podra desplegar una app movil
         ocupara toda la pantalla, si es desktop sera 'md 6'  */}
        <form style={style.form}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={12}>
              {/*  onChange={ingresarValoresMemoria} convierte en 1 var d estado todo el TextField
               */}
              <TextField
                name="NombreCompleto"
                
                value={usuario.NombreCompleto}
                onChange={ingresarValoresMemoria}
                variant="outlined"
                fullWidth
                label="Ingrese nombre y apellidos"
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField
                name="Email"
                value={usuario.Email}
                onChange={ingresarValoresMemoria}
                variant="outlined"
                fullWidth
                label="Ingrese su email"
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField
                name="Username"
                value={usuario.Username}
                onChange={ingresarValoresMemoria}
                variant="outlined"
                fullWidth
                label="Ingrese su username"
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField
                name="Password"
                type="password"
                value={usuario.Password}
                onChange={ingresarValoresMemoria}
                variant="outlined"
                fullWidth
                label="Ingrese su password"
              />
            </Grid>

            <Grid item xs={12} md={6}>
              <TextField
                name="ConfirmarPassword"
                type="password"
                value={usuario.ConfirmarPassword}
                onChange={ingresarValoresMemoria}
                variant="outlined"
                fullWidth
                label="Confirme su password"
              />
            </Grid>
          </Grid>
          <Grid container justifyContent="center">
            <Grid item xs={12} md={6}>
              <Button
                type="submit"
                onClick={registrarUsuarioBoton}
                fullWidth
                variant="contained"
                color="primary"
                size="large"
                style={style.submit}
              >
                Enviar
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default RegistrarUsuario;


