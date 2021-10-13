import React from 'react';
import { AppBar } from '@material-ui/core'; 
import BarSesion from './bar/BarSesion';


const AppNavbar = () => {
    
    return (

     //AppBar es un comp MaterialDesign, quiero q siempre esté arriba d la app <AppBar position="static">
     <AppBar position="static">
         <BarSesion/>

     </AppBar>
    );
};

export default AppNavbar;
