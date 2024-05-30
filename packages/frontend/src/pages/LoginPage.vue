<template>
    <div>
        <div class="bg-blue-200 p-6 mt-20 mx-auto rounded" style="width:80%">
            <div class="flex flex-col justify-center gap-4">

                <h1 class="w-full text-[4rem] font-medium ">SecureTeamSimulator</h1>
                <div class="flex  justify-center">
                    <svg height="300px" width="300px" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" 
                            viewBox="0 0 495 495" xml:space="preserve">
                            <g>
                                <path style="fill:#9BC9FF;" d="M180,107.5c0-37.22,30.28-67.5,67.5-67.5S315,70.28,315,107.5V175h40v-67.5
                                    C355,48.224,306.776,0,247.5,0S140,48.224,140,107.5V175h40V107.5z"/>
                                <polygon style="fill:#003F8A;" points="247.5,175 247.5,306.717 276.928,277.289 305.211,305.573 275.784,335 305.211,364.427 
                                    276.928,392.711 247.5,363.283 247.5,495 425,495 425,175 	"/>
                                <polygon style="fill:#2488FF;" points="218.072,392.711 189.789,364.427 219.216,335 189.789,305.573 218.072,277.289 
                                    247.5,306.717 247.5,175 70,175 70,495 247.5,495 247.5,363.283 	"/>
                                <polygon style="fill:#BDDBFF;" points="189.789,305.573 219.216,335 189.789,364.427 218.072,392.711 247.5,363.283 
                                    276.928,392.711 305.211,364.427 275.784,335 305.211,305.573 276.928,277.289 247.5,306.717 218.072,277.289 	"/>
                            </g>
                        </svg>
                </div>
      
                 <button @click="login" class="mt-12 rounded-lg bg-blue-400 p-3">Login</button>
            </div>



        </div>
     
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