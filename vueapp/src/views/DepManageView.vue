<template>
    <div>
        <div id="btnDiv">
            <b-button variant="primary" @click="showModal">부서 추가</b-button>
        </div>
        <div>
            <b-table striped hover :items="list" :fields="fields" @row-clicked="rowClick">
                <template v-slot:cell(DepartmentStatus)="{ value }">
                    {{ value ? '활성화' : '비활성화' }}
                </template>
                <template v-slot:item="{ item }">
                    <tr @click="depDetail(item)">
                    </tr>
                </template>
            </b-table>
        </div>
        <div id="modal">
            <DepRegistModal v-if="regModalCheck" @close-modal="regModalCheck=false" @RegDep="reList"></DepRegistModal>
            <DepModifyModal v-if="detModalCheck" :rowOfChild="rowData" 
            @close-modal="detModalCheck=false" @RegDep="reList"
            @refresh="refreshItem"></DepModifyModal>
        </div>
    </div>
</template>
<script>
import DepRegistModal from "../components/DepRegistModal.vue";
import DepModifyModal from "../components/DepModifyModal.vue";

export default {
    components:{
        DepRegistModal,
        DepModifyModal
    },
    data(){
        return {
            regModalCheck:false,
            detModalCheck:false,
            list:[],
            fields: [
                { key: 'DepartmentId', label: '부서 번호' },
                { key: 'DepartmentName', label: '부서명' },
                { key: 'DepartmentStatus', label: '활성화 여부' }
            ],
            rowData:{
                DepartmentId:'',
                DepartmentName:'',
                DepartmentStatus:''
            }
        }
    },
    mounted(){
        this.getList();
    },
    methods:{
        rowClick(item){
            this.detModalCheck = !this.detModalCheck;
            this.rowData.DepartmentId = item.DepartmentId;
            this.rowData.DepartmentName = item.DepartmentName;
            this.rowData.DepartmentStatus = item.DepartmentStatus;
            console.log(this.rowData);
        },
        reList(data){
            this.list = data;
        },
        showModal(){
            this.regModalCheck = !this.regModalCheck;
        },
        getList(){
            this.axios
            .get('http://localhost:54884/api/department/depList')
            .then((result)=>{
                console.log(result.data.list);
                this.list = result.data.list;
                this.list.sort((a, b) => b.DepartmentStatus - a.DepartmentStatus);
            })
            .catch((error)=>{
                console.log(error);
            })
        },
        refreshItem(){
            this.list.sort((a, b) => b.DepartmentStatus - a.DepartmentStatus);
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
