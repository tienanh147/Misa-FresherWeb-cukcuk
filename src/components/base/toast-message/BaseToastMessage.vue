<template>
	<div is="transition-group" name="toast" class="toast-message-list">
		<div
			v-for="(item, index) in toastMessageList"
			:class="bindClass(item)"
			:key="index"
			class="toast-message"
		>
			<div class="toast-message__body">
				<div class="toast-message__icon">
					<i class="fas" :class="bindIcon(item)"></i>
				</div>
				<span class="toast-message__title"> {{ item.content }}</span>
			</div>
			<div
				@click="removeToastMessage(item.id, 0)"
				class="toast-message__cancel"
			>
				<i class="fas fa-times"></i>
			</div>
		</div>
	</div>
</template>
<script>

export default {
	name: "BaseToastMessage",
	data() {
		return {
			iconClasses: [
				"fa-check-circle",
				"fa-exclamation-triangle",
				"fa-exclamation-circle",
				"fa-info-circle"
			],
			toastMessageList: [],
			toastMessageId: 0
		};
	},
	created() {
		/**
		 * Bật lắng nghe sự kiện Toast Message từ các component khác
		 * CreatedBy: NTDUNG (03/08/2021)
		 * @param {object} data - Chứa thông tin chi tiết của một Toast Message (type, content, duration)
		 */
		this.$eventBus.$on("ToastMessage", data => {
			this.addToastMessage(data);
		});
	},
	methods: {
		/**
		 * Thêm một toast message mới
		 * CreatedBy: NTDUNG (03/08/2021)
		 * @param {object} data
		 */
		addToastMessage(data) {
			this.toastMessageList.push({
				type: data["type"],
				content: data["content"],
				id: ++this.toastMessageId
			});
			this.removeToastMessage(this.toastMessageId, data["duration"]);
		},
		/**
		 * Với từng thông báo khác nhau thì các icon sẽ khác nhau
		 * CreatedBy: NTDUNG (03/08/2021)
		 * @param {string} item
		 */
		bindIcon(item) {
			var iconClasses = this.iconClasses;
			var iconClass;
			var returnClass = {};
			switch (item.type) {
				case "success":
					iconClass = iconClasses[0];
					break;
				case "error":
					iconClass = iconClasses[1];
					break;
				case "warn":
					iconClass = iconClasses[2];
					break;
				case "info":
					iconClass = iconClasses[3];
					break;
				default:
					return "";
			}
			returnClass[iconClass] = true;
			return returnClass;
		},
		/**
		 * Với những thông báo khác nhau thì màu hiển thị cũng khác nhau (css qua class)
		 * CreatedBy: NTDUNG (03/08/2021)
		 * @param {string} item
		 */
		bindClass(item) {
			var type;
			var returnClass = {};
			switch (item.type) {
				case "success":
					type = "toast-message--success";
					break;
				case "error":
					type = "toast-message--error";
					break;
				case "warn":
					type = "toast-message--warn";
					break;
				case "info":
					type = "toast-message--info";
					break;
				default:
					return "";
			}
			returnClass[type] = true;
			return returnClass;
		},
		/**
		 * Xoá toast message
		 * CreatedBy: NTDUNG (03/08/2021)
		 * @param {number} id - id của toast message
		 * @param {number} duration - khoảng thời gian chờ cho đến lúc xoá
		 */
		removeToastMessage(id, duration) {
			setTimeout(() => {
				var foundMatchId = this.toastMessageList.findIndex(item => {
					return item.id == id;
				});
				this.toastMessageList.splice(foundMatchId, 1);
			}, duration);
		}
	}
};
</script>
<style scoped>
@import url("./toast-message.css");
.toast-leave {
	opacity: 1;
}
.toast-leave-active {
	transition: all 0.2s ease;
	opacity: 0;
}

.toast-message-list {
	display: flex;
	flex-direction: column;
	align-items: flex-end;
	justify-content: flex-end;
	position: fixed;
	top: 0;
	right: 100px;
  z-index: 300;
}
/* TOAST MESSAGE */
.toast-message {
	margin-top: 20px;
	display: flex;
	height: 48px;
	background-color: #fff;
	align-items: center;
	justify-content: space-between;
	color: #000;
	font-size: 13px;
	border-radius: 4px;
	box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16);
	animation: toastMessageFadeIn 0.8s ease;
	width: fit-content;
	min-width: 300px;
}
.toast-message.toast-message--success {
	color: var(--toast-success-color);
}

.toast-message.toast-message--error {
	color: var(--toast-error-color);
}

.toast-message.toast-message--warn {
	color: var(--toast-warn-color);
}

.toast-message.toast-message--info {
	color: var(--toast-info-color);
}
.toast-message__body {
	display: flex;
	flex-direction: row;
}
.toast-message__icon {
	margin: 0 10px;
}
.toast-message__icon i {
	font-size: 18px;
}
.toast-message__title {
	font-size: 13px;
	font-family: Arial, Helvetica, sans-serif;
	color: #000 !important;
}

.toast-message__cancel {
	cursor: pointer;
	margin: 0 10px;
}
.toast-message__cancel i {
	font-size: 16px;
	font-weight: 600;
}

/* BUTTON TOAST */
.button-wrapper {
	margin-bottom: 40px;
}
.btn-toast {
	padding: 6px 16px;
	color: #fff;
	font-size: 14px;
	border: 0;
	margin-right: 10px;
	border-radius: 4px;
	cursor: pointer;
}
.btn-toast:hover {
	opacity: 0.7;
}

.btn-toast:active {
	opacity: 0.5;
}

.btn-toast--success {
	background-color: var(--toast-success-color);
}

.btn-toast--error {
	background-color: var(--toast-error-color);
}

.btn-toast--warn {
	background-color: var(--toast-warn-color);
}

.btn-toast--info {
	background-color: var(--toast-info-color);
}

/* ANIMATIONS */
@keyframes toastMessageFadeIn {
	from {
		opacity: 0;
		transform: translateX(100%);
	}
	to {
		opacity: 1;
		transform: translateX(0);
	}
}
</style>