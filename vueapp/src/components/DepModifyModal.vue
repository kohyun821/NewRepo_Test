<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <div class="modal-header">
                <h5 class="modal-title">부서 상세보기</h5>
            </div>

            <hr/>

            <div class="modal-input">
                <div>
                    <b>부서번호 : </b>
                    <b>{{rowOfChild.DepartmentId}}</b>
                </div>

                <div>
                    <b>부서명</b>
                    <input type="text" class="form-control" placeholder="이름" v-model="formData.DepartmentName"/>
                </div>

                <div>
                    <b>활성화 여부</b>

                    <input class="form-check-input form-check-input_margin_left" type="checkbox" v-model="formData.DepartmentStatus"
                        id="materialStatusCheck">
                </div>
            </div>

            <div class="modal-btn">
                <b-button @click="modalClose">닫기</b-button>
                <b-button variant="primary" @click="depMod" 
                :disabled="clickable">수정하기</b-button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props:['rowOfChild'],
    data(){
        return{
            nameCheck:false,
            statusCheck:false,
            formData:{
                DepartmentId:this.rowOfChild.DepartmentId,
                DepartmentName:this.rowOfChild.DepartmentName,
                DepartmentStatus:this.rowOfChild.DepartmentStatus
            }
        }
    },
    beforeUpdate(){
        this.changeCheck();
    },
    computed:{
        clickable(){
            if(this.nameCheck==false&&this.statusCheck==false){
                return true;
            }
            return false;
        }
    },
    methods:{
        changeCheck(){
            if(this.rowOfChild.DepartmentName != this.formData.DepartmentName){
                this.nameCheck = true;
            }else{
                this.nameCheck = false;
            }

            if(this.rowOfChild.DepartmentStatus != this.formData.DepartmentStatus){
                this.statusCheck = true;
            }else{
                this.statusCheck = false;
            }
        },
        modalClose(){
            this.$emit('close-modal');
        },
        depMod(){
            if(this.nameCheck==true){
                this.axios
                .post('http://localhost:54884/api/department/modifyName',this.formData)
                .then((result)=>{
                    if(result.data == null){
                        alert("같은 이름의 부서가 있습니다.");
                    }else if(Object.keys(result.data).includes('Table1')){
                        alert(result.data.Table1[0].msg);
                    }else{
                        alert("수정 완료!");
                        this.$emit('close-modal');
                        this.$emit('RegDep', result.data.list);
                    }
                })
                .catch((error)=>{
                    console.log(error);
                })
            }else if(this.nameCheck==false&&this.statusCheck==true){
                this.axios
                .post('http://localhost:54884/api/department/modifyStatus',this.formData)
                .then((result)=>{
                    console.log(result.data);
                    if(Object.keys(result.data).includes('Table1')){
                        alert(result.data.Table1[0].msg);
                    }else{
                        alert("수정 완료!");
                        this.$emit('close-modal');
                        this.$emit('RegDep', result.data.list);
                    }
                })
                .catch((error)=>{
                    console.log(error);
                })
            }
        }
        // depMod(){
        //     if(this.nameCheck==true){
        //         this.axios
        //         .post('http://localhost:54884/api/department/modifyName',this.formData)
        //         .then((result)=>{
        //             if(result.data == null){
        //                 alert("같은 이름의 부서가 있습니다.");
        //             }else{
        //                 alert("수정 완료!");
        //                 this.$emit('close-modal');
        //                 this.$emit('RegDep', result.data.list);
        //             }
        //         })
        //         .catch((error)=>{
        //             console.log(error);
        //         })
        //     }else if(this.nameCheck==false&&this.statusCheck==true){
        //         this.axios
        //         .post('http://localhost:54884/api/department/modifyStatus',this.formData)
        //         .then((result)=>{
        //             alert("수정 완료!");
        //             this.$emit('close-modal');
        //             this.$emit('RegDep', result.data.list);
        //         })
        //         .catch((error)=>{
        //             console.log(error);
        //         })
        //     }
        // }
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