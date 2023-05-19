<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <div class="modal-header">
                <h5 class="modal-title">사원 상세보기</h5>
            </div>

            <hr />

            <div class="modal-input">
                <div>
                    <b>사원 번호 : </b>
                    <b>{{ rowOfChild.EmployeeId }}</b>
                </div>

                <div>
                    <b>자재명</b>
                    <input type="text" class="form-control" placeholder="이름" v-model="formData.EmployeeName" />
                </div>

                <div>
                    <b>부서명</b>
                    <b-form-select v-model="formData.DepartmentId" :options="options" size="sm"
                        class="mt-3"></b-form-select>
                </div>
                
            </div>
            <div class="modal-btn">
                <b-button @click="modalClose">닫기</b-button>
                <b-button variant="primary" @click="EmpMod" :disabled="clickable">수정하기</b-button>
                <b-button variant="danger" @click="EmpResign" v-if="this.rowOfChild.DepartmentId != 0">퇴사</b-button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ['rowOfChild'],
    data() {
        return {
            list: [],
            formData: {
                EmployeeId: this.rowOfChild.EmployeeId,
                EmployeeName: this.rowOfChild.EmployeeName,
                DepartmentId: this.rowOfChild.DepartmentId
            },
            options: [],
            nameCheck: false,
            DepCheck: false
        }
    },
    created() {
        this.getDepList();
    },
    beforeUpdate() {
        this.changeCheck();
    },
    computed: {
        clickable() {
            if (this.nameCheck == false && this.DepCheck == false) {
                return true;
            }
            return false;
        }
    },
    methods: {
        changeCheck() {
            if (this.rowOfChild.EmployeeName != this.formData.EmployeeName) {
                this.nameCheck = true;
            } else {
                this.nameCheck = false;
            }

            if (this.rowOfChild.DepartmentId != this.formData.DepartmentId) {
                this.DepCheck = true;
            } else {
                this.DepCheck = false;
            }
        },
        modalClose() {
            this.$emit('close-modal');
        },
        pushDep() {
            for (var i = 0; i < this.list.length; i++) {
                this.options.push({
                    value: this.list[i].DepartmentId,
                    text: this.list[i].DepartmentName
                });
            }
        },
        getDepList() {
            this.axios
                .get('http://localhost:54884/api/employee/usableDep')
                .then((result) => {
                    console.log(result.data.list);
                    this.list = result.data.list;
                    this.pushDep();
                })
                .catch((error) => {
                    console.log(error);
                })
        },
        EmpMod() {
            this.axios
                .post('http://localhost:54884/api/employee/modifyEmp', this.formData)
                .then((result) => {
                    alert("수정 완료!");
                    this.$emit('close-modal');
                    this.$emit('RegEmp', result.data.list);
                })
                .catch((error) => {
                    console.log(error);
                })
        },
        EmpResign() {
            if (confirm('정말로 퇴사 조치하겠습니까?')) {
                this.axios
                    .post('http://localhost:54884/api/employee/empResign', { EmployeeId: this.formData.EmployeeId })
                    .then((result) => {
                        this.$emit('close-modal');
                        this.$emit('RegEmp', result.data.list);
                    })
                    .catch((error) => {
                        console.log(error);
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

.form-check-input_margin_left {
    margin-left: 2%;
}

.modal-input>div>input {
    margin-top: 1%;
}
</style>