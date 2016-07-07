(function () {

	function generateRow(item) {
		return "<tr data-id=" + item.Id + "><td class='ui-make'>"
				+ item.Make + "</td><td class='ui-desc'>"
				+ item.Description + "</td><td>"
				+ "<a class='btn btn-link ui-edit-vehicle'>Edit</a></td><td>"
				+ "<a class='btn btn-link ui-delete-vehicle' data-confirm='Are you sure you want to delete?'>Delete</a></td></tr>";
	}

	function deleteItem(row) {
		$.ajax({
			type: "DELETE",
			url: "api/vehicles/" + row.data("id"),
			cache: false
		}).done(function (data) {
			console.log(data,row);
			$(row).remove();
		})
        .fail(function (err) {
			console.log(err);
        });
		return false;
	}


	function deleteconfirm() {
		//delete confirm
		$('a[data-confirm]').on('click', function (ev) {
			var row = $(this).closest("tr");
			if (!$('#dataConfirmModal').length) {
				$('body').append('<div id="dataConfirmModal" class="modal" role="dialog" aria-labelledby="dataConfirmLabel" aria-hidden="true"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button><h3 id="dataConfirmLabel">Please Confirm</h3></div><div class="modal-body"></div><div class="modal-footer"><button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button><a class="btn btn-primary" id="dataConfirmOK" data-dismiss="modal" >OK</a></div></div>');
			}
			$('#dataConfirmModal').find('.modal-body').text($(this).attr('data-confirm'));
			$('#dataConfirmOK').click(function() {
				deleteItem(row);
			});
			$('#dataConfirmModal').modal({ show: true, backdrop:true});
			return false;
		});
	}

	function editRowItem() {
		$(".ui-edit-vehicle").on("click", function() {
			var trEdit = $(this).closest("tr");
			var tdmake = editRowItemMake(trEdit);
			var tddescription = editRowItemDesc(trEdit);
			$(trEdit).find(".btn").hide();
			var tdbuttons = $(this).parent();
			tdbuttons.append("<a class='btn btn-primary ui-edit-update'>Update</a><a class='btn btn-default btn-cancel ui-edit-cancel'>Cancel</a>");

			tdbuttons.find(".ui-edit-update").click(function () {
				var data = {
					Id: $(trEdit).data("id"),
					Make: tdmake.find('input').val(), Description: tddescription.find("input").val()
				};

				$.ajax({
						type: "POST",
						url: "api/vehicles/update",
						contentType: "application/json",
						data: JSON.stringify(data),
						dataType: "json"
					})
					.done(function () {
						cancelEdit(trEdit);
					})
					.fail(function(err) {
						console.log(err);
					});
			});

			tdbuttons.find(".ui-edit-cancel").click(function () {
				cancelEdit(trEdit);
			});

			tdmake.find('input').focus();
		});
	}

	function editRowItemMake(trEdit) {
		var tdmake = trEdit.find(".ui-make");
		var input = "<input type='text' id='editmake' value='" + $.trim(tdmake.text()) + "'/>";
		tdmake.text("");
		tdmake.append(input);
		return tdmake;
	}

	function editRowItemDesc(trEdit) {
		var tddesc = trEdit.find(".ui-desc");
		var input = "<input type='text' id='editdesc' value='" + $.trim(tddesc.text()) + "'/>";
		tddesc.text("");
		tddesc.append(input);
		return tddesc;
	}

	function cancelEdit(trEdit) {

		$.each(trEdit.find("input"), function(i, input) {
			$(input).parent().text($(input).val());
		});

		$(trEdit).find(".ui-edit-update").remove();
		$(trEdit).find(".ui-edit-cancel").remove();
		$(trEdit).find(".btn").show();
	}

	function loadVehicles() {
		$.ajax({
				type: "Get",
				url: "api/vehicles"
		}).done(function (data) {
			if (data && data.length > 0) {
				var tbody = $("#vehicletable")
					.find("tbody");
				//remove loading row
				tbody.find("tr").remove();

				$.each(data, function(i, item) {
					tbody.append(generateRow(item));
				});

				deleteconfirm();
				editRowItem();
			} else {
				$(".ui-loading")
					.addClass("no-item")
					.text("There are no items to display.");
			}
		}).fail(function(err) {
				//todo toast
				console.log(err);
			});
	}

	function addVehicle() {
		//todo validation
		if (!$("#vehicleForm").valid())
			return false;

		if ($("#Make").val() === "" || $("#Description").val() === "")
			return false;

		var datastring = $("#vehicleForm").serialize();
		$.ajax({
				type: "POST",
				url: "api/vehicles",
				data: datastring,
				dataType: "json"
			})
			.done(function (data) {
				if ($(".no-item")) {
					$(".ui-loading").closest("tr").remove();
				}

				var tbody = $("#vehicletable").find("tbody");
				tbody.append(generateRow(data));

				//clear form fields
				$("#Make").val("");
				$("#Description").val("");

				deleteconfirm();
				editRowItem();
			})
			.fail(function(err) {
				console.log(err);
			});
	}

	loadVehicles();

	$("#addVehicle").on("click", function() {
		addVehicle();
	});
})()