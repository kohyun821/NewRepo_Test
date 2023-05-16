<template>
    <div class="about">
        <h1>사원관리</h1>
        <table v-if="employees.length">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>이름</th>
                    <th>부서</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="employee in employees" :key="employee.EmployeeId">
                    <td>{{ employee.EmployeeId }}</td>
                    <td>{{ employee.EmployeeName }}</td>
                    <td>{{ employee.DepartmentName }}</td>
                </tr>
            </tbody>
        </table>
        <div v-else>
            <p>No employees found.</p>
        </div>
    </div>

    <div>
        <form @submit.prevent="CreateEmployee">
            <div>
                <label>이름</label>
                <input type="text" id="employeeName" v-model="FormData.employeeName" />
            </div>
            <div>
                <label>부서</label>
                <select v-model="FormData.departmentId">
                    <option v-for="department in departments" :key="department.DepartmentId" :value="department.DepartmentId">
                        {{ department.DepartmentName }}
                    </option>
                </select>
            </div>
            <button>생성</button>
        </form>

    </div>
</template>

<script>
import axios from 'axios'
export default {
    name: 'CreateMaterial',
    data() {
        return {
            employees: [],
            departments: [],
            FormData: {
                employeeName: '',
                departmentId: ''
            }
        }
    },
    async created() {
        try {
            const employeeresponse = await axios.get('http://localhost:54884/api/Employee');
            console.log(employeeresponse.data);
            this.employees = employeeresponse.data;

            const departmentResponse  = await axios.get('http://localhost:54884/api/Department');
            console.log(departmentResponse .data);
            this.departments = departmentResponse .data;
        } catch (error) {
            console.error(error)
        }
    },
    methods: {
        CreateEmployee() {
            if (this.FormData.employeeName === '' ||
                this.FormData.departmentId === ''
            )
                alert("공란으로 입력 하지 말아 주세요.");
            else {
                axios.post("http://localhost:54884/api/Employee/Regist", this.FormData)
                    .then((response) => {
                        this.employees.push(response.data);
                        return;
                    })
                    .catch((error) => {
                        console.log(error);
                        alert(error.response);
                    })
            }

        }
    },
}
</script>