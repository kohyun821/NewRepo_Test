<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <h3>사원 추가</h3>
            <hr/>
            <div id="modal-content">
                <table>
                    <tr>
                        <td class="info">
                            <b>사원명 : </b>
                        </td>
                        <td><input v-model="formData.EmployeeName" type="text"/></td>
                    </tr>
                    <tr>
                        <td class="info">
                            <b>부서 : </b>
                        </td>
                        <td>
                            <b-form-select v-model="formData.DepartmentId" :options="options" size="sm" class="mt-3"></b-form-select>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modal-btn">
                <b-button @click="modalClose">닫기</b-button>
                <b-button variant="primary" @click="EmpAdd">추가하기</b-button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return{
            formData:{
                EmployeeName:'',
                DepartmentId:'',
            },
            options:[],
            list:[]
        }
    },
    created(){
        this.getDepList();
    },
    methods:{
        pushDep(){
            for(var i=0;i<this.list.length;i++){
                this.options.push({
                    value:this.list[i].DepartmentId,
                    text:this.list[i].DepartmentName
                });
            }
        },
        getDepList(){
            this.axios
            .get('http://localhost:54884/api/employee/usableDep')
            .then((result)=>{
                console.log(result.data.list);
                this.list = result.data.list;
                this.pushDep();
            })
            .catch((error)=>{
                console.log(error);
            })
        },
        modalClose(){
            this.$emit('close-modal');
        },
        EmpAdd(){
            this.axios
            .post('http://localhost:54884/api/employee/regist',this.formData)
            .then((result)=>{
                alert("추가 완료!");
                this.$emit('close-modal');
                this.$emit('RegEmp', result.data.list);
            })
            .catch((error)=>{
                console.log(error);
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
.modal-btn{
    margin-top: 5%;
    text-align: center;
}
button {
    margin-left: 5%;
}
#modal-content{
    text-align: left;
    margin-left: 25%;
}
.info{
    text-align: right;
}
</style>
