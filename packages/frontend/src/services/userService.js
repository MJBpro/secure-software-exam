import * as apiGetUser from "@/api/getUser"
import * as apiGetMe from "@/api/getMe"
import * as apiGetUsers from "@/api/getAllUsers"

import * as apiAddUser from "@/api/addUser"
import { ref } from "vue"


export const useUserService = () =>  {


    const user = ref()

    async function getMe(){
        return await apiGetMe.get();
    }

    async function getUser(id){
        user.value = await apiGetUser.get(id);
        return user.value
    }

    async function getAllUsers(){
        return await apiGetUsers.get();
    }

    async function createUser(user){
        return await apiAddUser.post(user);
    }


    async function ensureUser(id){
        if(user.value) return user.value
        user.value = await apiGetUser.get(id);
        return user.value
    }





    return{
        getMe,
        getUser,
        getAllUsers,
        createUser,
        ensureUser

    }
    

}