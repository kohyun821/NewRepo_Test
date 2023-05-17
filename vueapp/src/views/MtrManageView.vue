<template>
    <div id="btnDiv">
        <b-button variant="primary" @click="showModal">자재추가</b-button>
    </div>
    <div>
        <h1>자재관리</h1>
        <b-table striped hover :items="materials" :fields="fields">
            <template v-slot:cell(MaterialStatus)="{ value }">
                {{ value ? '활성화' : '비 활성화' }}
            </template>
        </b-table>
    </div>
    <div>
        <div id="modal">
            <Modal v-if="modalCheck" @close-modal="modalCheck = false"></Modal>
        </div>
        <Modal v-if="modalCheck" @close-modal="modalCheck = false" @refresh="refreshMaterials"></Modal>
    </div>
</template>

<script>
import Modal from "../components/MtrRegistModal.vue";
import axios from 'axios'
export default {
    name: 'CreateMaterial',
    components: {
        Modal
    },
    data() {
        return {
            modalCheck: false,
            materials: [],
            fields: [
                { key: 'MaterialId', label: 'ID' },
                { key: 'MaterialName', label: '이름' },
                { key: 'MaterialAmount', label: '수량' },
                { key: 'MaterialTotal', label: '총 량' },
                { key: 'MaterialStatus', label: '상태' }
            ]
        }
    },
    async created() {
        try {
            const response = await axios.get('http://localhost:54884/api/Material')
            this.materials = response.data;
            //true가 먼저 위에 올 수 있도록.
            this.materials = response.data.sort((a, b) => b.MaterialStatus - a.MaterialStatus);
        } catch (error) {
            console.error(error)
        }
    },
    methods: {
        showModal() {
            this.modalCheck = !this.modalCheck;
        },
        refreshMaterials(newMaterial) {
            this.materials.push(newMaterial);
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
