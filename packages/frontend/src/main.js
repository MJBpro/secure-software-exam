// src/main.js
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { createAuth0 } from '@auth0/auth0-vue';
import { setService } from './services/axios';

const app = createApp(App);

app.use(router);


var auth = createAuth0({
    domain: "pbsw.eu.auth0.com",
    clientId: "B8Wl4W10gRdaldhiH03BDkfWl3EO9WAR",
    authorizationParams: {
        redirect_uri: window.location.origin + '/callback',
    },
    cacheLocation: 'localstorage',
    onRedirectCallback: (appState) => {
        console.log(appState)
        router.push(appState?.targetUrl || '/');
    },
})

app.use(
    auth
);

setService(auth)





app.mount('#app');
