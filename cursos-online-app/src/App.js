import React, { useEffect, useState } from "react";
import { ThemeProvider as MuithemeProvider } from "@material-ui/core/styles";
import theme from "./theme/theme";
import PerfilUsuario from "./componentes/seguridad/PerfilUsuario";
import Login from "./componentes/seguridad/Login";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import RegistrarUsuario from "./componentes/seguridad/RegistrarUsuario";
import { Grid, Snackbar } from "@material-ui/core";
import AppNavbar from "./componentes/navegacion/AppNavbar";
import { useStateValue } from "./contexto/store";
import { obtenerUsuarioActual } from "./actions/UsuarioAction";

function App() {
  const [{ openSnackbar }, dispatch] = useStateValue();

  const [iniciaApp, setIniciaApp] = useState(false);

  //Esta f nos permite ejecutar cierto codigo cuaando se haya cargado mi comp react
  useEffect(() => {
    if (!iniciaApp) {
      obtenerUsuarioActual(dispatch)
        .then((response) => {
          setIniciaApp(true);
        })
        .catch((error) => {
          setIniciaApp(true);
        });
      //sea error o exitoso siempre quiero q inicie.. por eso ambas condiciones van en true
    }

    //Y solo evalua el cambio d valor d 'iniciaApp'
  }, [iniciaApp]);
  return (
    <React.Fragment>
      <Snackbar
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
        open={openSnackbar ? openSnackbar.open : false}
        autoHideDuration={3000}
        ContentProps={{ "aria-describedy": "message-id" }}
        message={
          <span id="message-id">
            {openSnackbar ? openSnackbar.mensaje : ""}
          </span>
        }
        onClose={() =>
          dispatch({
            type: "OPEN_SNACKBAR",
            openMensaje: {
              open: false,
              mensaje: "",
            },
          })
        }
      ></Snackbar>
      <Router>
        <MuithemeProvider theme={theme}>
          <AppNavbar />

          <Grid container>
            <Switch>
              <Route exact path="/auth/login" component={Login} />
              <Route
                exact
                path="/auth/registrar"
                component={RegistrarUsuario}
              />
              <Route exact path="/auth/perfil" component={PerfilUsuario} />

              <Route exact path="/" component={PerfilUsuario} />
            </Switch>
          </Grid>
        </MuithemeProvider>
      </Router>
    </React.Fragment>
  );
}

export default App;
