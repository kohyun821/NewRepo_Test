<template>
    <div class="about">
        <h1>자재입출</h1>
        <table v-if="informations.length">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>자재</th>
                    <th>이름</th>
                    <th>입/출고</th>
                    <th>수량</th>
                    <th>날짜</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="information in informations" :key="information.InformationId">
                    <td>{{ information.InformationId }}</td>
                    <td>{{ information.MaterialName }}</td>
                    <td>{{ information.EmployeeName }}</td>
                    <td>{{ information.InformationStatus ? '입고' : '출고' }}</td>
                    <td>{{ information.InformationAmount }}</td>
                    <td>{{ information.InformationDate }}</td>
                </tr>
            </tbody>
        </table>
        <div v-else>
            <p>No informations found.</p>
        </div>
    </div>

    <div>
        <form @submit.prevent="CreateInformation">
            <div>
                <label>부서 이름</label>
                <select v-model="FormData.departmentId" @change="filterEmployees">
                    <option v-for="department in departments" :key="department.DepartmentId"
                        :value="department.DepartmentId">
                        {{ department.DepartmentName }}
                    </option>
                </select>
            </div>
            <div>
                <label>사원 이름</label>
                <select v-model="FormData.employeeId">
                    <option v-if="employees.length == 0">속한 직원 없음</option>
                    <option v-for="employee in employees" :key="employee.EmployeeId" :value="employee.EmployeeId">
                        {{ employee.EmployeeId }}/{{ employee.EmployeeName }}
                    </option>
                </select>
            </div>
            <div>
                <label>자재 이름</label>
                <select v-model="FormData.materialId">
                    <option v-for="material in materials" :key="material.MaterialId" :value="material.MaterialId">
                        {{ material.MaterialName }}
                    </option>
                </select>
            </div>
            <div>
                <label>입/출고</label>
                <input type="radio" id="in" value="1" v-model="FormData.informationStatus">
                <label for="in">입고</label>
                <input type="radio" id="out" value="0" v-model="FormData.informationStatus">
                <label for="out">출고</label>
            </div>
            <div>
                <label>수량</label>
                <input type="number" id="informationAmount" min="0" v-model="FormData.informationAmount" />
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
            informations: [],
            allEmployees: [],
            employees: [],
            materials: [],
            departments: [],
            MaterialDetail: {
                MaterialName: '',
                MaterialAmout: '',
                MaterialTotal: '',
            },
            FormData: {
                departmentId: '',
                materialId: '',
                employeeId: '',
                informationStatus: '',
                informationAmount: ''
            }
        }
    },
    async created() {
        try {
            const informationresponse = await axios.get('http://localhost:54884/api/Information');
            this.informations = informationresponse.data;

            //부서 Id 가 0 이 아닌 모든 사원 Get
            const employeeResponse = await axios.get('http://localhost:54884/api/Employee/Working');
            this.allEmployees = employeeResponse.data;
            this.employees = employeeResponse.data;

            //MaterialStatus가 1인 데이터 Get
            const materialResponse = await axios.get('http://localhost:54884/api/Material/Using/1');
            this.materials = materialResponse.data;

            //DepartmentStatus가 1인 데이터(부서) Get
            const departmentResponse = await axios.get('http://localhost:54884/api/Department/UsingAndWorking/1');
            this.departments = departmentResponse.data;
        } catch (error) {
            console.error(error)
        }
    },
    methods: {
        filterEmployees() {
            this.employees = this.allEmployees.filter(
                employee => employee.DepartmentId === this.FormData.departmentId
            );
            if (this.employees.length == 0) {
                this.FormData.employeeId = '';
            }
        },
        CreateInformation() {
            if (this.FormData.departmentId === '' ||
                this.FormData.materialId === '' ||
                this.FormData.employeeId === '' ||
                this.FormData.informationStatus === '' ||
                this.FormData.informationAmount === ''
            )
                alert("공란으로 입력 하지 말아 주세요.");
            else {

                // material details
                var currentMaterial = this.materials.find(material => material.MaterialId === this.FormData.materialId);
                // information status
                this.FormData.informationStatus = Number(this.FormData.informationStatus);

                // Total 보다 많게 입고 할 경우
                if (this.FormData.informationStatus === 1 &&
                    (currentMaterial.MaterialAmount + Number(this.FormData.informationAmount) > currentMaterial.MaterialTotal)) {
                    alert("총 합을 넘을 수 없습니다." + "\n"
                        + "현재 수량 : " + currentMaterial.MaterialAmount + "\n"
                        + "전체 수량 : " + currentMaterial.MaterialTotal);
                    return;
                }

                // 0개보다 적게 만들면서 출고 할 경우
                if (this.FormData.informationStatus === 0 &&
                    (currentMaterial.MaterialAmount - Number(this.FormData.informationAmount) < 0)) {
                    alert("남은 잔고를 확인해 주세요." + "\n"
                        + "현재 수량 : " + currentMaterial.MaterialAmount);
                    return;
                }

                axios.post("http://localhost:54884/api/Information/Regist", this.FormData)
                    .then((response) => {
                        console.log(response.data);
                        this.informations.push(response.data);
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