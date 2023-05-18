<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <div class="modal-header">
                <h5 class="modal-title">자재 대여 / 반납</h5>
            </div>

            <hr />

            <div class="modal-input">
                <div>
                    <b>부서 이름</b>
                    <select class="form-select" v-model="FormData.departmentId" @change="filterEmployees">
                        <option v-for="department in departments" :key="department.DepartmentId"
                            :value="department.DepartmentId">
                            {{ department.DepartmentName }}
                        </option>
                    </select>
                </div>

                <div>
                    <b>사원 이름</b>
                    <select class="form-select" v-model="FormData.employeeId">
                        <option v-if="employees.length == 0">속한 직원 없음</option>
                        <option v-for="employee in employees" :key="employee.EmployeeId" :value="employee.EmployeeId">
                            {{ employee.EmployeeId }}/{{ employee.EmployeeName }}
                        </option>
                    </select>
                </div>

                <div>
                    <b>자재 이름</b>
                    <select class="form-select" v-model="FormData.materialId">
                        <option v-for="material in materials" :key="material.MaterialId" :value="material.MaterialId">
                            {{ material.MaterialName }}
                        </option>
                    </select>

                </div>

                <div>
                    <b>대여 / 반납</b>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="out" value="0"
                            v-model="FormData.informationStatus">
                        <label class="form-check-label" for="out">대여</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="in" value="1" v-model="FormData.informationStatus">
                        <label class="form-check-label" for="in">반납</label>
                    </div>
                </div>

                <div>
                    <b>수량</b>
                    <input class="form-control" type="number" min="0" v-model="FormData.informationAmount" />

                </div>


            </div>
            <div class="modal-btn">
                <b-button @click="modalClose">닫기</b-button>
                <b-button variant="primary" @click="CreateInformation" :disabled="clickable">추가하기</b-button>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
export default {
    emits: ['list'],
    data() {
        return {
            amountCheck: false,
            filteredEmployees: [],
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
            },
        }
    },
    async created() {
        try {
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
    beforeUpdate() {
        this.changeCheck();
    },
    computed: {
        clickable() {
            if (this.amountCheck == false) {
                return true;
            }
            //0이상인게 인증 되면 버튼 클릭 가능하게
            return false;
        }
    },
    methods: {
        changeCheck() {
            //음수를 입력하면 버튼 잠금
            if (this.FormData.informationAmount < 0) {
                this.amountCheck = false;
            } else {
                this.amountCheck = true;
            }
        },
        modalClose() {
            this.$emit('close-modal');
        },
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
            else if (this.FormData.employeeId === '속한 직원 없음')
                alert("사원을 다시 확인해 주세요.");
            else if (this.FormData.informationAmount <= 0)
                alert("수량은 양수여야 합니다.");
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
                        alert("값이 추가 되었습니다.");
                        console.log("response.data", response.data)
                        this.$emit('updateInformation', response.data);
                        this.modalClose();
                    })
                    .catch((error) => {
                        alert(error.response.data);
                    })
            }

        }
    }
}
</script>
<style scoped>
.modal-wrap {
    position: fixed;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.4);
}

/* modal or popup */
.modal-container {
    position: relative;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 550px;
    background: #fff;
    border-radius: 10px;
    padding: 20px;
    box-sizing: border-box;
}

.modal-btn {
    margin-top: 5%;
    text-align: right;
}

button {
    margin-left: 5%;
}

.modal-input>* {
    margin-bottom: 3%;
}

.modal-input>div {
    text-align: left;
}

.modal-input>div>input {
    margin-top: 1%;
}
</style>
