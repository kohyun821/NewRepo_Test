<template>
    <div class="d-flex justify-content-between align-items-center m-3">
        <h1 class="text-center w-100">부서관리</h1>
        <b-button class="btn-custom" variant="primary" @click="showModal">부서 추가</b-button>
    </div>

    <b-table striped hover :items="list" :fields="fields" @row-clicked="rowClick">
        <template v-slot:cell(DepartmentStatus)="{ value }">
            {{ value ? '활성화' : '비활성화' }}
        </template>
    </b-table>

    <DepRegistModal v-if="regModalCheck" @close-modal="regModalCheck = false" @RegDep="reList"></DepRegistModal>
    <DepModifyModal v-if="detModalCheck" :rowOfChild="rowData" @close-modal="detModalCheck = false" @RegDep="reList">
    </DepModifyModal>
</template>
<script>
import DepRegistModal from "../components/DepRegistModal.vue";
import DepModifyModal from "../components/DepModifyModal.vue";

export default {
    components: {
        DepRegistModal,
        DepModifyModal
    },
    data() {
        return {
            regModalCheck: false,
            detModalCheck: false,
            list: [],
            fields: [
                { key: 'DepartmentId', label: '부서 번호' },
                { key: 'DepartmentName', label: '부서명' },
                { key: 'DepartmentStatus', label: '활성화 여부' }
            ],
            rowData: {
                DepartmentId: '',
                DepartmentName: '',
                DepartmentStatus: ''
            }
        }
    },
    created() {
        this.getList();
    },
    methods: {
        rowClick(item) {
            this.detModalCheck = !this.detModalCheck;
            this.rowData.DepartmentId = item.DepartmentId;
            this.rowData.DepartmentName = item.DepartmentName;
            this.rowData.DepartmentStatus = item.DepartmentStatus;
            console.log(this.rowData);
        },
        reList(data) {
            this.list = data;
            this.list.sort((a, b) => b.DepartmentStatus - a.DepartmentStatus);
        },
        showModal() {
            this.regModalCheck = !this.regModalCheck;
        },
        getList() {
            this.axios
                .get('http://localhost:54884/api/department/depList')
                .then((result) => {
                    console.log(result.data.list);
                    this.list = result.data.list;
                    this.list.sort((a, b) => b.DepartmentStatus - a.DepartmentStatus);
                })
                .catch((error) => {
                    console.log(error);
                })
        }
    }
}
</script>
<style>
.btn-custom {
    font-size: 14px;
    padding: 6px 12px;
    white-space: nowrap;
}
</style>