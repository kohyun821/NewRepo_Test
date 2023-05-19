<template>
    <div class="d-flex justify-content-between align-items-center m-3">
        <h1 class="text-center w-100">사원관리</h1>
        <b-button class="btn-custom" variant="primary" @click="showModal">사원 추가</b-button>
    </div>

    <b-table striped hover :items="list" :fields="fields" @row-clicked="rowClick">
        <template v-slot:cell(DepartmentName)="data">
            {{ data.value }}
        </template>
        <template v-slot:cell(Status)="data">
            {{ data.item.DepartmentName === '퇴사' ? '퇴사' : '재직중' }}
        </template>
    </b-table>

    <EmpRegistModal v-if="regModalCheck" @close-modal="regModalCheck = false" @RegEmp="reList"></EmpRegistModal>
    <EmpModifyModal v-if="detModalCheck" :rowOfChild="rowData" @close-modal="detModalCheck = false" @RegEmp="reList">
    </EmpModifyModal>

</template>

<script>
import EmpRegistModal from "../components/EmpRegistModal.vue";
import EmpModifyModal from "../components/EmpModifyModal.vue";

export default {
    components: {
        EmpRegistModal,
        EmpModifyModal
    },
    data() {
        return {
            regModalCheck: false,
            detModalCheck: false,
            formData: {
                departmentName: ''
            },
            list: [],
            fields: [
                { key: 'EmployeeId', label: '사원 번호' },
                { key: 'EmployeeName', label: '사원명' },
                { key: 'DepartmentName', label: '부서명' },
                { key: 'Status', label: '상태' }
            ],
            rowData: {
                EmployeeId: '',
                EmployeeName: '',
                DepartmentName: '',
                DepartmentId: ''
            }
        }
    },
    created() {
        this.getList()
    },
    methods: {
        reList(data) {
            this.list = data;
        },
        showModal() {
            this.regModalCheck = !this.regModalCheck;
        },
        getList() {
            this.axios
                .get('http://localhost:54884/api/employee/empList')
                .then((result) => {
                    console.log(result.data.list);
                    this.list = result.data.list;
                })
                .catch((error) => {
                    console.log(error);
                })
        },
        rowClick(item) {
            this.detModalCheck = !this.detModalCheck;
            this.rowData.EmployeeId = item.EmployeeId;
            this.rowData.EmployeeName = item.EmployeeName;
            this.rowData.DepartmentName = item.DepartmentName;
            this.rowData.DepartmentId = item.DepartmentId;
            console.log(this.rowData);
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