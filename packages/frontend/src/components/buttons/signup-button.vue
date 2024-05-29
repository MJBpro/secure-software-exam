<template>
 <button class="signup-button" @click="handleSignup">Sign up</button>
</template>

  
  
<script setup>
import { useAuth0 } from '@auth0/auth0-vue';

const { loginWithRedirect } = useAuth0();

const handleSignup = () => {
    loginWithRedirect({
        appState: {
            target: '/profile',
        },
        authorizationParams: {
            screen_hint: 'signup',
        }
    })
}

</script>