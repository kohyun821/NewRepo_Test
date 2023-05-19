<template>
    <div class="d-flex justify-content-between align-items-center m-3">
        <h1 class="text-center w-100">자재입출</h1>
        <b-button class="btn-custom" variant="primary" @click="showModal">자재 대여 / 반납</b-button>
    </div>
    <b-table striped hover :items="informations" :fields="fields">
        <template v-slot:cell(InformationStatus)="{ value }">
            {{ value ? '반납' : '대여' }}
        </template>
    </b-table>

    <Modal v-if="modalCheck" @close-modal="modalCheck = false" @updateInformation="refreshInformations"></Modal>
</template>

<script>
import Modal from "../components/InfoRegistModal.vue";
import axios from 'axios'
export default {
    name: 'CreateMaterial',
    components: {
        Modal
    },
    data() {
        return {
            modalCheck: false,
            informations: [],
            fields: [
                { key: 'MaterialName', label: '자재 이름' },
                { key: 'DepartmentName', label: '부서 이름' },
                { key: 'EmployeeName', label: '사원 이름' },
                { key: 'InformationStatus', label: ' 대여 / 반납' },
                { key: 'InformationAmount', label: '수량' },
                { key: 'InformationDate', label: '날짜' }
            ]
        }
    },
    async created() {
        try {
            const informationresponse = await axios.get('http://localhost:54884/api/Information');
            this.informations = informationresponse.data;
        } catch (error) {
            console.error(error)
        }
    },
    methods: {
        showModal() {
            this.modalCheck = !this.modalCheck;
        },
        refreshInformations(newInformation) {
            this.informations.length = 0;
            this.informations = newInformation;
        }
    },
}
</script>
<style>
.btn-custom {
    font-size: 14px;
    padding: 6px 12px;
    white-space: nowrap;
}
</style>
