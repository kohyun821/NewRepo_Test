<template>
    <div>
        <div id="btnDiv">
            <b-button variant="primary" @click="showModal">부서추가</b-button>
        </div>
        <div>
            <b-table striped hover :items="list">
                <thead>
                    <th>부서코드</th>
                    <th>부서명</th>
                    <th>부서상태</th>
                </thead>
                <tbody>
                    <tr v-for="(value, idx) in list" :key="idx">
                        <td>{{ value.departmentId }}</td>
                        <td>{{ value.departmentName }}</td>
                        <td>{{ value.departmentStatus }}</td>
                    </tr>
                </tbody>
            </b-table>
        </div>
        <div id="modal">
            <Modal v-if="modalCheck" @close-modal="modalCheck=false"></Modal>
        </div>
    </div>
</template>
<script>
import Modal from "../components/DepRegistModal.vue";

export default {
    components:{
        Modal
    },
    data(){
        return {
            modalCheck:false,
            list:[]
        }
    },
    mounted(){
        this.getList()
    },
    methods:{
        showModal(){
            this.modalCheck = !this.modalCheck;
        },
        getList(){
            this.axios
            .get('http://localhost:54884/api/department/depList')
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
<style>
#btnDiv{
    text-align: right;
    margin-right: 15%;
    margin-bottom: 2%;
}
</style>
