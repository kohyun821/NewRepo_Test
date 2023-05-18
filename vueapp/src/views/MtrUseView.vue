<template>
    <div id="btnDiv">
        <b-button variant="primary" @click="showModal">자재 대여 / 반납</b-button>
    </div>
    <div>
        <h1>자재입출</h1>
        <b-table striped hover :items="informations" :fields="fields">
            <template v-slot:cell(InformationStatus)="{ value }">
                {{ value ? '반납' : '대여' }}
            </template>
        </b-table>
    </div>
    <div>
        <div id="modal">
            <Modal v-if="modalCheck" 
            @close-modal="modalCheck = false" 
            @updateInformation="refreshInformations"
            ></Modal>
        </div>
    </div>
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
                { key: 'InformationId', label: 'ID' },
                { key: 'MaterialName', label: '자재 이름' },
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
            console.log("informations",this.informations)
            console.log("newInformation",newInformation)
            this.informations.length=0;
            console.log("informations-after",this.informations)
            this.informations = newInformation;
            console.log("informations-after-after",this.informations)
        }
    },
}
</script>
<style>
#btnDiv {
    text-align: right;
    margin-right: 15%;
    margin-bottom: 2%;
}
</style>
