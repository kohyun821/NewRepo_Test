<template>
    <div class="modal-wrap" @click="$emit('close-modal')">
        <div class="modal-container" @click.stop="">
            <h3>사원 추가</h3>
        </div>
    </div>
</template>