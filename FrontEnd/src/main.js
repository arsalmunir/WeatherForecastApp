import { createApp } from 'vue'
import App from './App.vue'
import "bootstrap/dist/css/bootstrap.min.css"
import { BootstrapIconsPlugin } from 'bootstrap-icons-vue';

const app = createApp(App)
app.use(BootstrapIconsPlugin);
app.mount('#app')
