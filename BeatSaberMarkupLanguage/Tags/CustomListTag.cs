﻿using BeatSaberMarkupLanguage.Components;
using BS_Utils.Utilities;
using HMUI;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Tags
{
    public class CustomListTag : BSMLTag
    {
        public override string[] Aliases => new[] { "custom-list" };
        public override bool AddChildren { get => false; }

        public override GameObject CreateObject(Transform parent)
        {
            RectTransform container = new GameObject("BSMLCustomListContainer", typeof(RectTransform)).transform as RectTransform;
            LayoutElement layoutElement = container.gameObject.AddComponent<LayoutElement>();
            container.SetParent(parent, false);

            GameObject gameObject = new GameObject();
            gameObject.name = "BSMLCustomList";
            gameObject.SetActive(false);

            TableView tableView = gameObject.AddComponent<BSMLTableView>();
            CustomCellListTableData tableData = container.gameObject.AddComponent<CustomCellListTableData>();
            tableData.tableView = tableView;

            gameObject.AddComponent<RectMask2D>();
            tableView.transform.SetParent(container, false);

            //temp
            FieldInfo fieldInfo = typeof(TableView).GetField("_preallocatedCells", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            fieldInfo.SetValue(tableView, new TableView.CellsGroup[0]);
            fieldInfo = typeof(TableView).GetField("_isInitialized", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            fieldInfo.SetValue(tableView, false);
            //
            /*
            tableView.SetPrivateField("_preallocatedCells", new TableView.CellsGroup[0]);
            tableView.SetPrivateField("_isInitialized", false);
            */

            RectTransform viewport = new GameObject("Viewport").AddComponent<RectTransform>();
            viewport.SetParent(gameObject.GetComponent<RectTransform>(), false);
            gameObject.GetComponent<ScrollRect>().viewport = viewport;

            (viewport.transform as RectTransform).anchorMin = new Vector2(0f, 0f);
            (viewport.transform as RectTransform).anchorMax = new Vector2(1f, 1f);
            (viewport.transform as RectTransform).sizeDelta = new Vector2(0f, 0f);
            (viewport.transform as RectTransform).anchoredPosition = new Vector3(0f, 0f);

            (tableView.transform as RectTransform).anchorMin = new Vector2(0f, 0f);
            (tableView.transform as RectTransform).anchorMax = new Vector2(1f, 1f);
            (tableView.transform as RectTransform).sizeDelta = new Vector2(0f, 0f);
            (tableView.transform as RectTransform).anchoredPosition = new Vector3(0f, 0f);

            tableView.dataSource = tableData;
            return container.gameObject;
        }
    }
}
