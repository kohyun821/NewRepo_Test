<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <h3>자재 추가</h3>
            <hr />
            <div class="modal-input">
                <b>이름 : </b>
                <input type="text" id="materialName" v-model="FormData.materialName" />
            </div>
            <div class="modal-input">
                <b>수량 : </b>
                <input type="number" id="materialAmount" min="0" v-model="FormData.materialAmount" />
            </div>
            <div class="modal-btn">
                <b-button @click="modalClose">닫기</b-button>
                <b-button variant="primary" @click="CreateMaterial">추가하기</b-button>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
export default {
    data() {
        return {
            FormData: {
                materialName: '',
                materialAmount: '',
                materialTotal: ''
            },
        }
    },
    methods: {
        modalClose() {
            this.$emit('close-modal');
        },
        CreateMaterial() {
            if (this.FormData.materialName === '' ||
                this.FormData.materialAmount === ''
            )
                alert("공란으로 입력 하지 말아 주세요.");
            else {
                this.FormData.materialAmount = Number(this.FormData.materialAmount);
                this.FormData.materialTotal = this.FormData.materialAmount;
                console.log("success");
                axios.post("http://localhost:54884/api/Material/Regist", this.FormData)
                    .then((response) => {
                        alert("값이 추가 되었습니다.");
                        this.$emit('updateMaterial',response.data);
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
    text-align: center;
}

.modal-input {
    margin-bottom: 1%;
}

button {
    margin-left: 5%;
}
</style>
