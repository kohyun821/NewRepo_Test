<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <h3>자재 상세보기</h3>
            <hr />
            <div id="modal-content">
                <div>
                    <b>자재번호 : </b><b>{{ formData.materialId }}</b>
                </div>
                <br />
                <div>
                    <b>자재명 : </b><input v-model="formData.materialName" type="text" />
                </div>
                <br />
                <div>
                    <b>수량 : </b><input type="number" min="0"  v-model="formData.materialTotal"/>
                </div>
                <br />
                <div>
                    <b>활성화 여부 : </b><input type="checkbox" v-model="formData.materialStatus"/>
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
            formData: {
                materialId: this.rowOfChild.MaterialId,
                materialName: this.rowOfChild.MaterialName,
                materialAmount: this.rowOfChild.MaterialAmount,
                materialTotal: this.rowOfChild.MaterialTotal,
                materialStatus: this.rowOfChild.MaterialStatus
            },
            originData: {
                materialAmount: this.rowOfChild.MaterialAmount,
                materialTotal: this.rowOfChild.MaterialTotal
            },
        }
    },
    mounted() {
        console.log(this.rowOfChild);
    },
    beforeUpdate() {
        this.changeCheck();
    },
    computed: {
        clickable() {
            if (this.nameCheck == false && this.statusCheck == false) {
                return true;
            }
            return false;
        }
    },
    methods: {
        changeCheck() {
            if (this.rowOfChild.MaterialName != this.formData.MaterialName) {
                this.nameCheck = true;
            } else {
                this.nameCheck = false;
            }

            if (this.rowOfChild.MaterialStatus != this.formData.MaterialStatus) {
                this.statusCheck = true;
            } else {
                this.statusCheck = false;
            }
        },
        modalClose() {
            this.$emit('close-modal');
        },
        mtrMod() {
            if (this.nameCheck == true) {
                this.formData.materialTotal = Number(this.formData.materialAmount);

                //입력한 값과 원래 총합의 차이를 구함.
                //입력한 값 - 기존 값
                const num = this.formData.materialTotal - this.originData.materialTotal;

                //차이가 현재 량보다 적을 경우 -> Amount도 같이 감소 해 줘야함.
                //즉 num 이 음수일 경우
                if (num > 0) {
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
                if (num < 0) {
                    //남은 재고도 같이 증가!
                    this.formData.materialAmount = this.formData.materialAmount + num;
                }

                this.axios
                    .post('http://localhost:54884/api/Material/Modify', this.formData)
                    .then((result) => {
                        if (result.data == null) {
                            alert("같은 이름의 부서가 있습니다.");
                        } else {
                            alert("수정 완료!");
                            this.$emit('updateMaterial', result.data);
                            this.modalClose();
                        }
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
    text-align: center;
}

button {
    margin-left: 5%;
}

#modal-content {
    text-align: left;
    margin-left: 25%;
}
</style>