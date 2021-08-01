<template>
  <div>
    <div class="filter-bar">
      <input
        class="text-box-default icon-search"
        type="text"
        :placeholder="inputPlaceHolder"
      />
      <SelectBox
        v-for="(data, index) in selectBoxesData"
        :dropdownData="data.slice(3)"
        :key="index"
        :idField="data[1]"
        :nameField="data[2]"
        :initContent="data[0]"
      />
    </div>
    <div
      class="text-box-default refresh-btn"
      id="buttonRefresh"
      @click="btnRefreshClick"
    ></div>
  </div>
</template>

<script>
import SelectBox from "../select-custom/BaseSelectBox.vue";
export default {
  name: "FilterTable",
  components: { SelectBox },
  props: {
    selectBoxesData: {
      type: Array,
      default: function() {
        return [
          [
            {
              PositionId: "30d41e52-5e66-72bc-6c1c-b47866e0b131",
              PositionCode: "P08",
              PositionName: "Giám đốc",
              Description: "Ut aut consectetur officia error dolor sequi.",
              ParentId: null,
              CreatedDate: "1970-01-01T00:00:55",
              CreatedBy: "Antoine Banks",
              ModifiedDate: null,
              ModifiedBy: null
            },
            {
              PositionId: "548dce5f-5f29-4617-725d-e2ec561b0f41",
              PositionCode: "P94",
              PositionName: "Nhân viên",
              Description:
                "Beatae modi aliquam aperiam atque soluta. Qui blanditiis amet. Dolorem expedita ut. Perspiciatis eos qui. Eos omnis eum; non aut unde.",
              ParentId: null,
              CreatedDate: "1970-09-09T01:45:48",
              CreatedBy: "Chaya Mccaffrey",
              ModifiedDate: null,
              ModifiedBy: null
            },
            {
              PositionId: "589edf01-198a-4ff5-958e-fb52fd75a1d4",
              PositionCode: "P07",
              PositionName: "Phó phòng",
              Description:
                "Maiores totam facere. Numquam error earum incidunt et laudantium accusamus. Dolor sint perspiciatis. Voluptas repellendus soluta; labore nisi dicta. Quia.",
              ParentId: null,
              CreatedDate: "1987-02-24T22:38:53",
              CreatedBy: "Aundrea Cahill",
              ModifiedDate: null,
              ModifiedBy: null
            },
            {
              PositionId: "5bd71cda-209f-2ade-54d1-35c781481818",
              PositionCode: "P92",
              PositionName: "Trưởng phòng",
              Description:
                "Dolorum animi hic recusandae non laudantium voluptatem. Modi dicta magni et autem magni eum.",
              ParentId: null,
              CreatedDate: "2006-08-16T13:18:07",
              CreatedBy: "Earle Caudill",
              ModifiedDate: null,
              ModifiedBy: null
            }
          ],
          [
            "Tất cả phòng ban",
            {
              DepartmentId: "142cb08f-7c31-21fa-8e90-67245e8b283e",
              DepartmentCode: "PB99",
              DepartmentName: "Phòng Marketting",
              Description:
                "Praesentium excepturi architecto ipsum possimus. Dolore molestiae omnis nihil. Aliquid perspiciatis qui. Ea sed nam; accusantium ipsam ut. Soluta.",
              CreatedDate: "1970-01-01T00:07:13",
              CreatedBy: "Letha Bolt",
              ModifiedDate: null,
              ModifiedBy: null
            },
            {
              DepartmentId: "17120d02-6ab5-3e43-18cb-66948daf6128",
              DepartmentCode: "PB89",
              DepartmentName: "Phòng đào tạo",
              Description:
                "Neque amet ut. Natus quis ratione. Itaque tempore ut. Enim impedit magnam. Quo consectetur temporibus! Excepturi debitis perspiciatis. Quis et expedita!",
              CreatedDate: "1972-09-20T22:18:35",
              CreatedBy: "Miyoko Mckinney",
              ModifiedDate: null,
              ModifiedBy: null
            },
            {
              DepartmentId: "469b3ece-744a-45d5-957d-e8c757976496",
              DepartmentCode: "PB86",
              DepartmentName: "Phòng Nhân sự",
              Description:
                "Quam odit provident dolor. Natus error velit consequatur hic vero ut! Est nemo molestiae adipisci qui quia ipsam.",
              CreatedDate: "1991-02-12T05:09:15",
              CreatedBy: "Vanita Kelleher",
              ModifiedDate: null,
              ModifiedBy: null
            },
            {
              DepartmentId: "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
              DepartmentCode: "PB92",
              DepartmentName: "Phòng Công nghệ",
              Description:
                "Qui tempora nisi similique laboriosam illum nesciunt. Unde similique omnis voluptatem sit nisi ipsum. Illum accusantium sit quia quidem; in et fuga.",
              CreatedDate: "1986-10-11T05:25:20",
              CreatedBy: "Marcos Abraham",
              ModifiedDate: null,
              ModifiedBy: null
            }
          ]
        ];
      }
    },
    inputPlaceHolder: {
      type: String,
      default: function() {
        return "Tìm kiếm theo Mã, Tên hoặc Số điện thoại";
      }
    },
    filterObj: {
      type: Object,
      default: function() {
        return {
          employeeFilter: "n",
          departmentId: "",
          positionId: ""
        };
      },
      required: true
    },
  },
  data() {
    return {};
  },
  methods: {
    btnRefreshClick: function() {
      this.sendEmit();
    },

    sendEmit() {
      return this.$emit("filter-choose", this.filterObj);
    }
  }
};
</script>

<style scope>
.filter .filter-bar {
  flex: 0.7;
  display: flex;
  justify-content: flex-start;
  align-items: center;
}

.filter .filter-bar input {
  flex: 3;
  font-size: 13px;
  /* width: 300px; */
  padding-right: 16px;
  padding-left: 40px;
  outline: none;
  box-sizing: border-box;
}

.filter-bar input::placeholder {
  font-size: 12px;
}

.filter-bar input:focus {
  border: 1px solid #019160;
}

.filter .filter-bar .select-box {
  flex: 2;
  min-width: 200px;
  margin: 0px 10px 0px 10px;
}

.filter .refresh-btn {
  width: 40px;
  min-width: 40px;
  background-color: var(--object-color);
  background-image: url(../../../assets/icon/refresh.png);
  background-repeat: no-repeat;
  background-position: center;
  cursor: pointer;
}

.refresh-btn:hover {
  background-color: #e5e5e5;
}
</style>
 