<template>
    <div v-if="sessionID==null" class="input-group mx-auto w-25">
      <input type="input" class="form-control" v-model="username" placeholder="Login"/>
      <input type="password" class="form-control" v-model="password" placeholder="Password"/>
      <button @click="Login()" class="btn btn-primary" :disabled='isDisabled'> Login </button>
    </div>

    <WeatherComponent v-if="sessionID!=null" :session = "sessionID"></WeatherComponent>
</template>

<script>
import WeatherComponent from './WeatherComponent.vue'
  import axios from 'axios'

export default ({
    name: "LoginComponent",

    data(){
        return{
            username: "",
            password: "",
            sessionID: null
        }
    },



    components: {
        WeatherComponent
    },



    methods : {

        async Login(){

            const response = await axios.get(`${process.env.VUE_APP_API_ENDPOINT}WeatherForecast/authentication?username=${this.username}&password=${this.password}`);
            if(response.status==200)
            {
                this.sessionID = response.data
            }
        }
    }
  
})
</script>

<style scoped>

</style>
