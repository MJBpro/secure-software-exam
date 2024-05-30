import * as apiGetUser from "@/api/getUser"
import * as apiGetMe from "@/api/getMe"
import * as apiGetUsers from "@/api/getAllUsers"
import * as apiSearchUsers from "@/api/searchUser"

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

    async function createUser(userInfo){
        return await apiAddUser.post(userInfo);
    }


    async function searchForUser(query){
        return await apiSearchUsers.get(query);
    }


    async function ensureUser(id){
        if(user.value) return user.value
        user.value = await apiGetUser.get(id);
        return user.value
    }

    function userIsAdmin(auth){
        var userRoles = auth.user.value["http://localhost:8082/roles"]
        if(!userRoles) return false;
        if(userRoles.includes("Admin")) return true
    }





    return{
        getMe,
        getUser,
        getAllUsers,
        createUser,
        ensureUser,
        userIsAdmin,
        searchForUser

    }
    

}