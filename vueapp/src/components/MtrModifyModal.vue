<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <div class="modal-header">
                <h5 class="modal-title">자재 상세보기</h5>
            </div>

            <hr />

            <div class="modal-input">
                <div>
                    <b>자재 번호 : </b>
                    <b>{{ formData.materialId }}</b>
                </div>

                <div>
                    <b>자재명</b>
                    <input type="text" class="form-control" placeholder="이름" v-model="formData.materialName" />
                </div>

                <div>
                    <b>수량</b>
                    <input type="number" class="form-control" placeholder="수량" min="0" id="materialTotal"
                        v-model="formData.materialTotal" />
                </div>

                <div>
                    <b>활성화 여부</b>

                    <input class="form-check-input form-check-input_margin_left" type="checkbox" v-model="formData.materialStatus"
                        id="materialStatusCheck">
                </div>

            </div>

            <div class="modal-btn">
                <b-button @click="modalClose">닫기</b-button>
                <b-button variant="primary" @click="mtrMod" :disabled="clickable">수정하기</b-button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ['rowOfChild'],
    data() {
        return {
            nameCheck: false,
            statusCheck: false,
            totalCheck: false,
            formData: {
                materialId: this.rowOfChild.MaterialId,
                materialName: this.rowOfChild.MaterialName,
                materialAmount: this.rowOfChild.MaterialAmount,
                materialTotal: this.rowOfChild.MaterialTotal,
                materialStatus: this.rowOfChild.MaterialStatus
            },
            originData: {
                materialId: this.rowOfChild.MaterialId,
                materialName: this.rowOfChild.MaterialName,
                materialAmount: this.rowOfChild.MaterialAmount,
                materialTotal: this.rowOfChild.MaterialTotal,
                materialStatus: this.rowOfChild.MaterialStatus
            },
        }
    },
    beforeUpdate() {
        this.changeCheck();
    },
    computed: {
        clickable() {
            if (this.nameCheck == false && this.statusCheck == false && this.totalCheck == false) {
                return true;
            }
            return false;
        }
    },
    methods: {
        changeCheck() {
            //바뀌었다면 trun 리턴
            if (this.rowOfChild.MaterialName != this.formData.materialName) {
                this.nameCheck = true;
            } else {
                this.nameCheck = false;
            }

            if (this.rowOfChild.MaterialStatus != this.formData.materialStatus) {
                this.statusCheck = true;
            } else {
                this.statusCheck = false;
            }

            if (this.rowOfChild.MaterialTotal != this.formData.materialTotal) {
                this.totalCheck = true;
            } else {
                this.totalCheck = false;
            }
        },
        modalClose() {
            this.$emit('close-modal');
        },
        mtrMod() {

            //입력값을 Number 형태로 바꾸어 저장.
            this.formData.materialTotal = Number(this.formData.materialTotal);

            console.log("this.formData.materialTotal", this.formData.materialTotal);
            //입력한 값과 원래 총합의 차이를 구함.
            //입력한 값 - 기존 값
            const num = this.formData.materialTotal - this.originData.materialTotal;

            console.log("num", num);
            //차이가 현재 량보다 적을 경우 -> Amount도 같이 감소 해 줘야함.
            //즉 num 이 음수일 경우
            if (num < 0) {
                //기존 수량 + num(음수) <0 일 경우 재고 에러
                if (this.formData.materialAmount + num < 0) {
                    alert("남은 잔고를 확인해 주세요." + "\n"
                        + "현재 수량 : " + this.originData.materialAmount);
                    return;
                }

                //아닐경우 데이터 수량 변경 -> 0이 될수도 있음.
                this.formData.materialAmount = this.formData.materialAmount + num;
            }

            //차이가 현재 량보다 많을 경우 -> Amount도 같이 증가 해 줘야함.
            //즉 num 이 양수일 경우
            if (num > 0) {
                //남은 재고도 같이 증가!
                this.formData.materialAmount = this.formData.materialAmount + num;
            }

            console.log("this.formData.materialAmount", this.formData.materialAmount);

            this.axios
                .post('http://localhost:54884/api/Material/Modify', this.formData)
                .then((result) => {
                    if (result.data == null) {
                        alert("같은 이름의 부서가 있습니다.");
                    } else {
                        alert("수정 완료!");
                        this.$emit('updateMaterial', result.data);
                        this.modalClose();
                        console.log("result.data", result.data);
                    }
                })
                .catch((error) => {
                    alert(error.response.data);
                })
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
.form-check-input_margin_left{
    margin-left: 2%;
}
.modal-input>div>input {
    margin-top: 1%;
}
</style>