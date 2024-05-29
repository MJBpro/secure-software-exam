<template>
    <div>

        <h1>Login page</h1>
        <button @click="login" class="login-button">Login</button>
    </div>    
</template>

<script setup>
import { useAuth0 } from "@auth0/auth0-vue";



var auth = useAuth0()


async function login(){

    await auth.loginWithRedirect();



}
</script>
<style scoped>
.navbar{
    width: auto;
    height: 12rem;
    background: rgb(78, 78, 218);
}
</style>