<template>
    <div>
        <div id="btnDiv">
            <b-button variant="primary" @click="showModal">사원 추가</b-button>
        </div>
        <b-table striped hover :items="list">
            
        </b-table>
    </div>
    <div id="modal">
            <EmpRegistModal v-if="regModalCheck" @close-modal="regModalCheck=false" @RegDep="reList"></EmpRegistModal>
        </div>
</template>

<script>
import EmpRegistModal from "../components/EmpRegistModal.vue";

export default {
    components:{
        EmpRegistModal
    },
    data(){
        return {
            regModalCheck:false,
            formData:{
                departmentName:''
            },
            list:[]
        }
    },
    mounted(){
        this.getList()
    },
    methods:{
        showModal(){
            this.regModalCheck = !this.regModalCheck;
        },
        getList(){
            this.axios
            .get('http://localhost:54884/api/employee/empList')
            .then((result)=>{
                console.log(result.data.list);
                this.list = result.data.list;
            })
            .catch((error)=>{
                console.log(error);
            })
        }
    }
}
</script>
<style scope>
#btnDiv{
    text-align: right;
    margin-right: 15%;
    margin-bottom: 2%;
}
</style>