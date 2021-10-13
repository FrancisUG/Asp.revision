import axios from "axios";

//Endpoint base dond van a correr mis Webservices
axios.defaults.baseURL = "http://localhost:5000/api";

//add un interceptor para q todos los request q salgan dsd AXIOS incluyen en el header el TOKEN
axios.interceptors.request.use(
  (config) => {
    //1ro Capturamos el token d mi browser, dame ese item q se llama getItem('token_seguridad')
    const token_seguridad = window.localStorage.getItem("token_seguridad");

  
    if (token_seguridad) {
      config.headers.Authorization = 'Bearer ' + token_seguridad;
      return config;
    }
  },
  (error) => {
    return Promise.reject(error);
  }
);

//Lo q representa los request q se uno envia al servidor
const requestGenerico = {
 
  get: (url) => axios.get(url),

  //body va xq el json tiene un cuerpo del mensaje
  post: (url, body) => axios.post(url, body),
  put: (url, body) => axios.put(url, body),
  delete: (url) => axios.delete(url)
};
export default requestGenerico;


