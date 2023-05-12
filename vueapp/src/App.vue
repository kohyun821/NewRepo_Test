<template>
    <PageHeader></PageHeader>
    <router-view></router-view>
    <PageFooter></PageFooter>
    <button @click="get">데이터 가져오기</button>
    <p>{{ data }}</p>
</template>

<script>
  import PageHeader from "./components/PageHeader.vue"
  import PageFooter from "./components/PageFooter.vue"

  export default {
    name:"App",
    components: {
      PageFooter,
      PageHeader
    },
    data() {
      return {
        data : [],
        id : 1
      }
    },
    methods: {
      get(){
        this.axios
        .get(`http://localhost:54884/api/department/depList`)
        .then((result)=>{
          console.log(result.data);
          this.data.push(result.data);
          this.id++;
        })
        .catch((error)=>{
          console.log(error);
        })
        .finally(()=>{
          console.log("finally");
        });
      }
    }
  }
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
    }

    #nav {
        padding: 30px;
    }

        #nav a {
            font-weight: bold;
            color: #2c3e50;
        }

            #nav a.router-link-exact-active {
                color: #42b983;
            }
</style>
