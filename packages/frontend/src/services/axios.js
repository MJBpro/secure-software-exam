import axios from "axios";


const apiClient = axios.create({
  baseURL:  "https://localhost:7206",
  timeout: 40000,
});



var authService = undefined;
export function setService(_authService){
    authService = _authService
}

apiClient.interceptors.request.use(async request => {
  // Check if token has expired

  

    if(!authService?.isAuthenticated){
        console.log("WARNING FIX - login again", authService);
    }
    
    console.log("WARNING FIX - login again", authService.idTokenClaims.value.__raw);
    const token =  authService.idTokenClaims.value.__raw

  return new Promise( function (resolve /*, reject*/) {

    if (token && request.headers) {
      request.headers = {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      }
    }
   
    resolve(request);

  });
});

apiClient.interceptors.response.use(undefined, async error => {

  
  const { response } = error;
  const data = response ? response : null

  if (response && response.status == 401){
    //TODO: fix
    return Promise.reject(error)
  }
  // Errors handling
  if (data ) {
   console.log("Warning - API:", data);
    throw (data);
  }
  else {
    return Promise.reject(error)
  }
});

export default apiClient;
