// src/main.js
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { createAuth0 } from '@auth0/auth0-vue';

const app = createApp(App);

app.use(router);

app.use(
    createAuth0({
        domain: "pbsw.eu.auth0.com",
        clientId: "B8Wl4W10gRdaldhiH03BDkfWl3EO9WAR",
        authorizationParams: {
            redirect_uri: window.location.origin + '/callback',
        },
        cacheLocation: 'localstorage',
        onRedirectCallback: (appState) => {
            router.push(appState?.targetUrl || '/');
        },
    })
);

app.mount('#app');
