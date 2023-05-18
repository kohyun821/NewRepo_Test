<template>
    <div id="btnDiv">
        <b-button variant="primary" @click="showModal">자재추가</b-button>
    </div>
    <div>
        <h1>자재관리</h1>
        <b-table striped hover :items="materials" :fields="fields" @row-clicked="rowClick">
            <template v-slot:cell(MaterialStatus)="{ value }">
                {{ value ? '활성화' : '비 활성화' }}
            </template>
        </b-table>
    </div>
    <div>
        <div id="modal">
            <RegistModal v-if="regist_modalCheck" @close-modal="regist_modalCheck = false " @refresh="refreshMaterials"></RegistModal>
        </div>

        <div id="modal">
            <ModifyModal v-if="modify_modalCheck" 
            :rowOfChild="rowData" 
            @close-modal="modify_modalCheck = false"
            @updateMaterial="refreshMaterials"
            ></ModifyModal>
        </div>
    </div>
</template>

<script>
import RegistModal from "../components/MtrRegistModal.vue";
import ModifyModal from "../components/MtrModifyModal.vue";
import axios from 'axios'
export default {
    name: 'CreateMaterial',
    components: {
        RegistModal,
        ModifyModal
    },
    data() {
        return {
            regist_modalCheck: false,
            modify_modalCheck: false,
            materials: [],
            fields: [
                { key: 'MaterialId', label: '자재 번호' },
                { key: 'MaterialName', label: '자재명' },
                { key: 'MaterialAmount', label: '남은 수량' },
                { key: 'MaterialTotal', label: '자재 수량' },
                { key: 'MaterialStatus', label: '활성화 여부' }
            ],
            rowData:{
                MaterialId:'',
                MaterialName:'',
                MaterialAmount:'',
                MaterialTotal:'',
                MaterialStatus:''
            }
        }
    },
    async created() {
        try {
            const response = await axios.get('http://localhost:54884/api/Material')
            //this.materials = response.data;
            //true가 먼저 위에 올 수 있도록.
            this.materials = response.data.sort((a, b) => b.MaterialStatus - a.MaterialStatus);
            console.log(this.materials)
        } catch (error) {
            console.error(error)
        }
    },
    methods: {
        rowClick(item){
            this.modify_modalCheck = !this.modify_modalCheck;
            this.rowData.MaterialId = item.MaterialId;
            this.rowData.MaterialName = item.MaterialName;
            this.rowData.MaterialAmount = item.MaterialAmount;
            this.rowData.MaterialTotal = item.MaterialTotal;
            this.rowData.MaterialStatus = item.MaterialStatus;
        },
        showModal() {
            this.regist_modalCheck = !this.regist_modalCheck;
        },
        refreshMaterials(updatedMaterial) {
            this.materials.length=0;
            this.materials = updatedMaterial;
            this.materials.sort((a, b) => b.MaterialStatus - a.MaterialStatus);
        }
    }
}
</script>
<style>
#btnDiv {
    text-align: right;
    margin-right: 15%;
    margin-bottom: 2%;
}
</style>
