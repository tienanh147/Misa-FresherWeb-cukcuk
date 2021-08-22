function PostNewEmployee(start, end) {
    for (let i = start; i < end; i++) {
        SendRandomRequest(i);
    }
}

function getRandomInt(start, end) {
    return Math.floor(Math.random() * end) + start;
}

function randomDate(start, end) {
    return new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
}

function SendRandomRequest(number) {
    var Ho = ['Trần', 'Lê', 'Nguyễn', 'Trần', 'Lê Hữu', 'Lý', 'Lương', 'Phạm', 'Đỗ', 'Võ', 'Lưu', 'Đậu', 'Trương', 'Hồ', 'Văn', 'Mạc'];
    var TenLot = ['Ngọc', 'Đức', 'Văn', 'Việt', 'Tiến', 'Tiến', 'Thị', 'Khánh', 'Thu', 'Thủy'];
    var Ten = ['Hưng', 'Hùng', 'Dũng', 'Lan', 'Thu', 'Huyền', 'Trâm', 'Huy', 'Đức', 'Long', 'Tú', 'Tùng', 'Đức', 'Ngọc', 'Loan', 'Hường', 'Tú', 'Lan', 'Mạnh', 'Anh', 'Anh'];

    //#region Department and Position
    var pos = [{ "PositionId": "30d41e52-5e66-72bc-6c1c-b47866e0b131", "PositionCode": "P08" },
        { "PositionId": "548dce5f-5f29-4617-725d-e2ec561b0f41", "PositionCode": "P94" },
        { "PositionId": "589edf01-198a-4ff5-958e-fb52fd75a1d4", "PositionCode": "P07" },
        { "PositionId": "5bd71cda-209f-2ade-54d1-35c781481818", "PositionCode": "P92" },
        { "PositionId": "c6f1472e-47d1-4909-b522-4e511e6ec81f", "PositionCode": "P17" }
    ];
    var dep = [{ "DepartmentId": "17120d02-6ab5-3e43-18cb-66948daf6128", "DepartmentCode": "PB89" },
        { "DepartmentId": "3fc024f1-4455-4d77-9db2-cf39245ad3ef", "DepartmentCode": "PB100" },
        { "DepartmentId": "469b3ece-744a-45d5-957d-e8c757976496", "DepartmentCode": "PB86" },
        { "DepartmentId": "4e272fc4-7875-78d6-7d32-6a1673ffca7c", "DepartmentCode": "PB92" },
        { "DepartmentId": "867f97b1-fc82-45dd-9d6e-51d8265301d7", "DepartmentCode": "PB99" }
    ];
    var quali = [{ "QualificationId": "3541ff76-58f0-6d1a-e836-63d5d5eff719", "Description": "Et ut nihil ab sint vero deserunt. Sunt aut magnam hic recusandae excepturi officia.", "CreatedDate": "1970-01-01 00:01:22", "CreatedBy": "nvmanh", "ModifiedDate": "", "ModifiedBy": "" },
        { "QualificationId": "39446ba3-3071-7af6-a5a9-deaf32c96293", "Description": "Qui quaerat sit nam animi ut deserunt. Nisi unde aut quis atque velit unde!", "CreatedDate": "1995-12-23 00:12:01", "CreatedBy": "nvmanh", "ModifiedDate": "", "ModifiedBy": "" },
        { "QualificationId": "447816de-6069-15bf-de3d-c9e9e9038fe2", "Description": "Quae error ad. Suscipit minima quaerat. Nihil modi pariatur. Eius laboriosam itaque? Quasi autem ad; quia sequi totam; quas aut non. Iste ut quos. Non ut est!", "CreatedDate": "2000-11-11 15:14:51", "CreatedBy": "nvmanh", "ModifiedDate": "", "ModifiedBy": "" },
        { "QualificationId": "79aca385-65e3-1ebe-f488-479bce9b28fc", "Description": "In id ut et recusandae quasi dignissimos. Aliquid exercitationem quidem tempore. Unde harum dolores odit fugit officia aspernatur!", "CreatedDate": "1980-12-11 22:03:38", "CreatedBy": "nvmanh", "ModifiedDate": "", "ModifiedBy": "" }
    ]

    //#region oldAPI manhnv    
    var pos2 = [{
            "PositionId": "30d41e52-5e66-72bc-6c1c-b47866e0b131",
            "PositionCode": "P08",
            "PositionName": "Giám đốc",
            "Description": "Ut aut consectetur officia error dolor sequi.",
            "ParentId": null,
            "CreatedDate": "1970-01-01T00:00:55",
            "CreatedBy": "Antoine Banks",
            "ModifiedDate": null,
            "ModifiedBy": null
        },
        {
            "PositionId": "548dce5f-5f29-4617-725d-e2ec561b0f41",
            "PositionCode": "P94",
            "PositionName": "Nhân viên",
            "Description": "Beatae modi aliquam aperiam atque soluta. Qui blanditiis amet. Dolorem expedita ut. Perspiciatis eos qui. Eos omnis eum; non aut unde.",
            "ParentId": null,
            "CreatedDate": "1970-09-09T01:45:48",
            "CreatedBy": "Chaya Mccaffrey",
            "ModifiedDate": null,
            "ModifiedBy": null
        },
        {
            "PositionId": "589edf01-198a-4ff5-958e-fb52fd75a1d4",
            "PositionCode": "P07",
            "PositionName": "Phó phòng",
            "Description": "Maiores totam facere. Numquam error earum incidunt et laudantium accusamus. Dolor sint perspiciatis. Voluptas repellendus soluta; labore nisi dicta. Quia.",
            "ParentId": null,
            "CreatedDate": "1987-02-24T22:38:53",
            "CreatedBy": "Aundrea Cahill",
            "ModifiedDate": null,
            "ModifiedBy": null
        },
        {
            "PositionId": "5bd71cda-209f-2ade-54d1-35c781481818",
            "PositionCode": "P92",
            "PositionName": "Trưởng phòng",
            "Description": "Dolorum animi hic recusandae non laudantium voluptatem. Modi dicta magni et autem magni eum.",
            "ParentId": null,
            "CreatedDate": "2006-08-16T13:18:07",
            "CreatedBy": "Earle Caudill",
            "ModifiedDate": null,
            "ModifiedBy": null
        }
    ];
    var dep2 = [{
            "DepartmentId": "142cb08f-7c31-21fa-8e90-67245e8b283e",
            "DepartmentCode": "PB99",
            "DepartmentName": "Phòng Marketting",
            "Description": "Praesentium excepturi architecto ipsum possimus. Dolore molestiae omnis nihil. Aliquid perspiciatis qui. Ea sed nam; accusantium ipsam ut. Soluta.",
            "CreatedDate": "1970-01-01T00:07:13",
            "CreatedBy": "Letha Bolt",
            "ModifiedDate": null,
            "ModifiedBy": null
        },
        {
            "DepartmentId": "17120d02-6ab5-3e43-18cb-66948daf6128",
            "DepartmentCode": "PB89",
            "DepartmentName": "Phòng đào tạo",
            "Description": "Neque amet ut. Natus quis ratione. Itaque tempore ut. Enim impedit magnam. Quo consectetur temporibus! Excepturi debitis perspiciatis. Quis et expedita!",
            "CreatedDate": "1972-09-20T22:18:35",
            "CreatedBy": "Miyoko Mckinney",
            "ModifiedDate": null,
            "ModifiedBy": null
        },
        {
            "DepartmentId": "469b3ece-744a-45d5-957d-e8c757976496",
            "DepartmentCode": "PB86",
            "DepartmentName": "Phòng Nhân sự",
            "Description": "Quam odit provident dolor. Natus error velit consequatur hic vero ut! Est nemo molestiae adipisci qui quia ipsam.",
            "CreatedDate": "1991-02-12T05:09:15",
            "CreatedBy": "Vanita Kelleher",
            "ModifiedDate": null,
            "ModifiedBy": null
        },
        {
            "DepartmentId": "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
            "DepartmentCode": "PB92",
            "DepartmentName": "Phòng Công nghệ",
            "Description": "Qui tempora nisi similique laboriosam illum nesciunt. Unde similique omnis voluptatem sit nisi ipsum. Illum accusantium sit quia quidem; in et fuga.",
            "CreatedDate": "1986-10-11T05:25:20",
            "CreatedBy": "Marcos Abraham",
            "ModifiedDate": null,
            "ModifiedBy": null
        }
    ];
    //#endregion
    //#endregion
    var employee = JSON.stringify({
        "createdBy": "AnhTT",
        "employeeCode": `NV${number}`,
        "fullName": `${Ho[getRandomInt(0, Ho.length)]} ${TenLot[getRandomInt(0, TenLot.length)]} ${Ten[getRandomInt(0, Ten.length)]}`,
        "gender": getRandomInt(0, 3),
        "dateOfBirth": randomDate(new Date(1980, 0, 1), new Date(2000, 12, 31)),
        "phoneNumber": `0${getRandomInt(333333333, 999999999)}`,
        "email": `nhanvien.MF${number}@misa.cukcuk.vn`,
        "address": "Hanoi",
        "identityNumber": `0382${Math.round(Math.random() * 100000000)}`,
        "identityDate": "2014-07-21",
        "identityPlace": "Viet Nam",
        "joinDate": randomDate(new Date(2015, 0, 1), new Date(2021, 7, 14)),
        "workStatus": getRandomInt(1, 4),
        "personalTaxCode": `${getRandomInt(100000000, 99999999)}`,
        "salary": Math.floor(Math.random() * 100000000),
        "positionId": pos[getRandomInt(0, pos.length) % pos.length].PositionId,
        "departmentId": dep[getRandomInt(0, dep.length) % dep.length].DepartmentId,
        "qualificationId": quali[getRandomInt(0, quali.length) % quali.length].QualificationId
    });
    console.log(employee);
    var settings = {
        "url": "https://localhost:44368/api/v1/Employees",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": employee,
    };

    $.ajax(settings).done(function(response) {
        console.log(number);
    });
}