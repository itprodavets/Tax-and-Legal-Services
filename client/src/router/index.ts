import Vue from 'vue'
import VueRouter from 'vue-router'
import CbcOecd from '../views/CbcOecd.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/cbc-oecd',
    name: 'cbc-oecd',
    component: CbcOecd
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})
export default router
