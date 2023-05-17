<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <h3>부서 추가</h3>
            <hr/>
            <div>
                <b>부서명 : </b><input v-model="formData.departmentName" type="text"/>
            </div>
            <div class="modal-btn">
                <b-button @click="modalClose">닫기</b-button>
                <b-button variant="primary" @click="depAdd">추가하기</b-button>
            </div>
        </div>
    </div>
</template>
<script>
export default{
    data(){
        return {
            formData:{
                departmentName:''
            },
        }
    },
    methods:{
        modalClose(){
            this.$emit('close-modal');
        },
        depAdd(){
            if(this.formData.departmentName === ""){
                alert("부서명을 입력하세요.");
            }else{
                this.axios
                .post('http://localhost:54884/api/department/regist', this.formData)
                .then((result)=>{
                    console.log(result.data);
                    if(result.data == null){
                        alert("같은 이름의 부서가 있습니다.");
                    }else{
                        alert("추가 완료!");
                        this.$emit('close-modal');
                        this.$emit('RegDep', result.data.list);
                        this.$emit('refresh');
                    }
                })
                .catch((error)=>{
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
.modal-btn{
    margin-top: 5%;
    text-align: center;
}
button {
    margin-left: 5%;
}
</style>
