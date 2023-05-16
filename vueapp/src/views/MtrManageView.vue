<template>
    <div class="about">
        <h1>자재관리</h1>
        <table v-if="materials.length">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>이름</th>
                    <th>남은 양</th>
                    <th>총 합</th>
                    <th>상태</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="material in materials" :key="material.MaterialId">
                    <td>{{ material.MaterialId }}</td>
                    <td>{{ material.MaterialName }}</td>
                    <td>{{ material.MaterialAmount }}</td>
                    <td>{{ material.MaterialTotal }}</td>
                    <td>{{ material.MaterialStatus ? '활성화' : '비 활성화' }}</td>
                </tr>
            </tbody>
        </table>
        <div v-else>
            <p>No materials found.</p>
        </div>
    </div>
    <h1>
        1515asdf
    </h1>
    <div>
        <!-- form 으로 값 입력 처리 => 엔터를 입력해도 가능 -->
        <!-- https://hello-bryan.tistory.com/266 -->
        <form @submit.prevent="CreateMaterial">
            <div>
                <label>이름</label>
                <input type="text" id="materialName" v-model="FormData.materialName" />
            </div>
            <div>
                <label>수량</label>
                <input type="number" id="materialAmount" min="0" v-model="FormData.materialAmount" />
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
            materials: [],
            FormData: {
                materialName: '',
                materialAmount: '',
                materialTotal: ''
            }
        }
    },
    async created() {
        try {
            const response = await axios.get('http://localhost:54884/api/Material')
            console.log(response.data);
            this.materials = response.data;
        } catch (error) {
            console.error(error)
        }
    },
    methods: {
        CreateMaterial() {
            if (this.FormData.materialName === '' ||
                this.FormData.materialAmount === ''
            )
                alert("공란으로 입력 하지 말아 주세요.");
            else {
                this.FormData.materialAmount = Number(this.FormData.materialAmount);
                this.FormData.materialTotal = this.FormData.materialAmount;
                axios.post("http://localhost:54884/api/Regist/Material", this.FormData)
                    .then((response) => {
                        console.log(response);
                        this.materials.push(response.data);
                        return;
                    })
                    .catch((error) => {
                        console.log(error);
                        alert(error.response.data);
                    })
            }

        }
    },
}
</script>