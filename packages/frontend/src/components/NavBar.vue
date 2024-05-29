<template>
    <div class="navbar">
        <div style="margin-left:auto; display: flex; align-items:center; gap: 20px">
            <h4>{{user.name}}</h4>
            <img :src="user.picture" class="avatar"/>
        </div>

    </div>
</template>

<script setup>
import { useAuth0 } from "@auth0/auth0-vue";
import { computed } from "vue";

var auth = useAuth0()
var user = computed(()=> auth.user.value)

</script>
<style scoped>
.navbar{
    align-items: center;
    display: flex;
    width: auto;
    height: 4rem;
    background: rgb(217, 222, 239);
    box-shadow: rgba(0, 0, 0, 0.24) 0px 1px 6px;
    padding: 1rem;
    }

    .avatar{
        border-radius: 100%;
        width: 4rem;;
    }
</style>